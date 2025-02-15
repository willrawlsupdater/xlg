// ReSharper disable UnusedParameter.Local

using MetX.Standard.Generation;
using MetX.Standard.Interfaces;
using MetX.Standard.Pipelines;
using MetX.Windows.Library;

namespace MetX.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;

    using ICSharpCode.TextEditor;
    using ICSharpCode.TextEditor.Document;
    using ICSharpCode.TextEditor.Gui.CompletionWindow;

    using MetX.Standard.Library;
    using MetX.Standard.Scripts;

    using Microsoft.Win32;

    public partial class QuickScriptToolWindow : ScriptRunningWindow
    {
        public static readonly List<QuickScriptOutput> OutputWindows = new List<QuickScriptOutput>();
        public static RegistryKey AppDataRegistry;
        public CodeCompletionWindow CompletionWindow;
        public XlgQuickScript CurrentScript;
        public bool Updating;

        private TextArea _textArea;

        public QuickScriptToolWindow(IGenerationHost host, XlgQuickScript script)
        {
            Host = host;
            InitializeComponent();
            InitializeEditor();
            CurrentScript = script;
        }

        public string WordBeforeCaret
        {
            get
            {
                if (ScriptEditor.Text != null && (ScriptEditor.Text.Length == 0 || _textArea.Caret.Column == 0))
                {
                    return string.Empty;
                }

                var lines = ScriptEditor.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                var linePart = lines[_textArea.Caret.Line].Substring(0, _textArea.Caret.Column).LastToken();
                int i;
                for (i = linePart.Length - 1; i >= 0; i--)
                {
                    if (!char.IsLetterOrDigit(linePart[i]))
                    {
                        break;
                    }
                }

                if (i > 0 && i < linePart.Length)
                {
                    linePart = linePart.Substring(i);
                }

                return linePart.ToLower().Trim();
            }
        }

        private string LineAtCaret
        {
            get
            {
                if (Text.Length == 0 || _textArea.Caret.Column == 0) return string.Empty;
                var line = Text.TokenAt(
                    _textArea.Caret.Line,
                    Environment.NewLine,
                    StringComparison.InvariantCulture);

                return line;
            }
        }

        public static string GetLastKnownPath()
        {
            var openedKey = false;
            if (AppDataRegistry == null)
            {
                AppDataRegistry = Application.UserAppDataRegistry;
                openedKey = true;
            }

            if (AppDataRegistry == null)
            {
                return null;
            }

            var lastKnownPath = AppDataRegistry.GetValue("LastQuickScriptPath") as string;

            if (!openedKey || AppDataRegistry == null)
            {
                return null;
            }

            AppDataRegistry.Close();
            AppDataRegistry = null;
            return lastKnownPath;
        }

        public void DisplayExpandedQuickScriptSourceInNotepad(bool independent)
        {
            try
            {
                if (CurrentScript == null)
                {
                    return;
                }

                UpdateScriptFromForm();
                var source = CurrentScript.ToCSharp(independent);
                if (!string.IsNullOrEmpty(source))
                {
                    QuickScriptWorker.ViewTextInNotepad(Host, source, true);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public string GenerateIndependentQuickScriptExe(string templateName)
        {
            if (InvokeRequired)
            {
                return (string)Invoke(new Func<string, string>(GenerateIndependentQuickScriptExe), templateName);
            }

            if (Context.Templates.Count == 0 ||
                string.IsNullOrEmpty(Context.Templates[templateName].Views["Exe"]))
            {
                MessageBox.Show(this, "Quick script template 'Exe' missing for: " + templateName);
                return null;
            }

            var source = CurrentScript.ToCSharp(true);
            var additionalReferences = Context.DefaultTypesForCompiler();
            var compilerResults = XlgQuickScript.CompileSource(source, true, additionalReferences, null, null);

            if (compilerResults.Failures != null && compilerResults.Failures.Length <= 0)
            {
                var assembly = compilerResults.CompiledAssembly;

                var parentDestination = CurrentScript.DestinationFilePath.TokensBeforeLast(@"\");

                if (string.IsNullOrEmpty(parentDestination)
                    && !string.IsNullOrEmpty(CurrentScript.InputFilePath)
                    && CurrentScript.Input != "Web Address")
                {
                    parentDestination = CurrentScript.InputFilePath.TokensBeforeLast(@"\");
                }

                if (string.IsNullOrEmpty(parentDestination))
                {
                    parentDestination = Context.Scripts.FilePath.TokensBeforeLast(@"\");
                }

                if (string.IsNullOrEmpty(parentDestination))
                {
                    if (!ReferenceEquals(assembly, null)) 
                        parentDestination = assembly.Location.TokensBeforeLast(@"\");
                }

                if (!Directory.Exists(parentDestination))
                {
                    if (!ReferenceEquals(assembly, null)) 
                        return assembly.Location;
                }

                if (!ReferenceEquals(assembly, null))
                {
                    var metXDllPathSource = Path.Combine(assembly.Location.TokensBeforeLast(@"\"), "MetX.dll");

                    parentDestination = Path.Combine(parentDestination, "bin");
                    var metXDllPathDest = Path.Combine(parentDestination, "MetX.dll");

                    var exeFilePath = Path.Combine(parentDestination, CurrentScript.Name.AsFilename()) + ".exe";
                    var csFilePath = exeFilePath.Replace(".exe", ".cs");

                    Directory.CreateDirectory(parentDestination);

                    if (File.Exists(exeFilePath))
                    {
                        File.Delete(exeFilePath);
                    }

                    if (File.Exists(csFilePath))
                    {
                        File.Delete(csFilePath);
                    }

                    File.Copy(assembly.Location, exeFilePath);
                    if (!File.Exists(metXDllPathDest))
                    {
                        File.Copy(metXDllPathSource, metXDllPathDest);
                    }

                    File.WriteAllText(csFilePath, source);
                    return exeFilePath;
                }
            }

            var lines = new List<string>(source.LineList());
            var errorOutput = compilerResults.Failures.ForDisplay(lines);

            MessageBox.Show(
                "Compilation failure. Errors found include:" 
                + Environment.NewLine + Environment.NewLine
                + errorOutput);
            
            QuickScriptWorker.ViewTextInNotepad(Host, lines.Flatten(), true);

            return null;
        }
        
        public void ShowCodeCompletion(string[] items)
        {
            if (items != null && items.Length > 0)
            {
                var completionDataProvider = new CompletionDataProvider(items);
                CompletionWindow = CodeCompletionWindow.ShowCompletionWindow(this, ScriptEditor, string.Empty, completionDataProvider, '.');
                if (CompletionWindow != null)
                {
                    CompletionWindow.Closed += CompletionWindowClosed;
                }
            }
        }

        public void UpdateScriptFromForm()
        {
            if (CurrentScript == null)
            {
                return;
            }

            CurrentScript.Script = ScriptEditor.Text;
        }

        private void CompletionWindowClosed(object source, EventArgs e)
        {
            if (CompletionWindow != null)
            {
                CompletionWindow.Closed -= CompletionWindowClosed;
                CompletionWindow.Dispose();
                CompletionWindow = null;
            }
        }

        private void InitializeEditor()
        {
            _textArea = ScriptEditor.ActiveTextAreaControl.TextArea;
            _textArea.KeyEventHandler += ProcessKey;
            _textArea.KeyUp += TextAreaOnKeyUp;

            var fsmProvider = new FileSyntaxModeProvider(AppDomain.CurrentDomain.BaseDirectory);
            HighlightingManager.Manager.AddSyntaxModeFileProvider(fsmProvider); // Attach to the text editor.
            ScriptEditor.SetHighlighting("QuickScript"); // Activate the highlighting, use the name from the SyntaxDefinition node.
            ScriptEditor.Refresh();
        }

        private void InsertMissingSections()
        {
            _textArea.InsertString("Members:\n\tList<string> Items = new List<string>();\n\n~~Start:\n\n~~Body:\n\n~~Finish:\n\n");
        }

        private bool ProcessKey(char ch)
        {
            switch (ch)
            {
                case '~':
                    if (LineAtCaret == CsProjGeneratorOptions.Delimiter)
                    {
                        InsertMissingSections();
                    }

                    break;
            }

            // switch (ch)
            // {
            // case '.':
            // if (WordBeforeCaret == "this")
            // {
            // ShowThisCodeCompletion();
            // }
            // break;
            // case '~':
            // ShowScriptCommandCodeCompletion();
            // break;
            // }
            return false;
        }

        private void QuickScriptToolWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveQuickScript_Click(sender, null);
        }

        private void QuickScriptToolWindow_Load(object sender, EventArgs e)
        {
            UpdateLastKnownPath();
        }

        private void RunQuickScript_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentScript == null)
                {
                    return;
                }

                UpdateScriptFromForm();
                Window.Host ??= Host;
                Context.RunQuickScript(this, CurrentScript, null);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void SaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                if (Updating)
                {
                    return;
                }

                if (Context.Scripts == null) return;

                UpdateScriptFromForm();

                SaveDestinationFilePathDialog.FileName = Context.Scripts.FilePath;
                SaveDestinationFilePathDialog.InitialDirectory = Context.Scripts.FilePath.TokensBeforeLast(@"\");
                SaveDestinationFilePathDialog.AddExtension = true;
                SaveDestinationFilePathDialog.CheckPathExists = true;
                SaveDestinationFilePathDialog.DefaultExt = ".xlgq";
                SaveDestinationFilePathDialog.Filter = "Quick script files (*.xlgq)|*.xlgq;All files (*.*)|*.*";
                SaveDestinationFilePathDialog.ShowDialog(this);
                if (!string.IsNullOrEmpty(SaveDestinationFilePathDialog.FileName))
                {
                    Context.Scripts.FilePath = SaveDestinationFilePathDialog.FileName;
                    Context.Scripts.Save();
                    Text = "Quick Script - " + Context.Scripts.FilePath;
                    UpdateLastKnownPath();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void SaveQuickScript_Click(object sender, EventArgs e)
        {
            try
            {
                if (Updating)
                {
                    return;
                }

                if (Context.Scripts != null)
                {
                    UpdateScriptFromForm();
                    if (string.IsNullOrEmpty(Context.Scripts.FilePath))
                    {
                        SaveAs_Click(null, null);
                    }
                    else
                    {
                        Context.Scripts.Save();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

/*
        private void ShowScriptCommandCodeCompletion()
        {
            ShowCodeCompletion(new[] { "~:", "~Members:", "~Start:", "~Body:", "~Finish:", "~BeginString:", "~EndString:" });
        }
*/      
        private void ShowThisCodeCompletion()
        {
            ShowCodeCompletion(new[] { "Output", "Lines", "AllText", "DestionationFilePath", "InputFilePath", "LineCount", "OpenNotepad", "Ask" });
        }

        private void TextAreaOnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.Space:
                        ShowThisCodeCompletion();
                        break;
                }
            }
            else
            {
                // No modifiers
                switch (e.KeyCode)
                {
                    case Keys.F5:
                        e.Handled = true;
                        RunQuickScript_Click(null, null);
                        break;
                }
            }
        }
/*
        private void UpdateFormWithScript(XlgQuickScript selectedScript)
        {
            if (CurrentScript == null)
            {
                return;
            }

            ScriptEditor.Text = selectedScript.Script;
            ScriptEditor.Refresh();
            ScriptEditor.Focus();
            CurrentScript = selectedScript;
        }
*/
        private void UpdateLastKnownPath()
        {
            if (Context.Scripts == null || string.IsNullOrEmpty(Context.Scripts.FilePath) || !File.Exists(Context.Scripts.FilePath))
            {
                return;
            }

            var openedKey = false;
            if (AppDataRegistry == null)
            {
                AppDataRegistry = Application.UserAppDataRegistry;
                openedKey = true;
            }

            if (AppDataRegistry == null)
            {
                return;
            }

            AppDataRegistry.SetValue("LastQuickScriptPath", Context.Scripts.FilePath, RegistryValueKind.String);

            if (!openedKey || AppDataRegistry == null)
            {
                return;
            }

            AppDataRegistry.Close();
            AppDataRegistry = null;
        }

        private void ViewGeneratedCode_Click(object sender, EventArgs e)
        {
            DisplayExpandedQuickScriptSourceInNotepad(false);
        }

        private void ViewIndependectGeneratedCode_Click(object sender, EventArgs e)
        {
            // DisplayExpandedQuickScriptSourceInNotepad(true);
            try
            {
                if (CurrentScript == null)
                {
                    return;
                }

                UpdateScriptFromForm();
                var location = GenerateIndependentQuickScriptExe(CurrentScript.Template);
                if (location.IsEmpty())
                {
                    return;
                }

                if (DialogResult.Yes == MessageBox.Show(this,
                    "Executable generated successfully at: " + location + Environment.NewLine +
                    Environment.NewLine +
                    "Would you like to run it now? (No will open the generated file).", "RUN EXE?", MessageBoxButtons.YesNo))
                {
                    Process.Start(new ProcessStartInfo(location)
                    {
                        UseShellExecute = true,
                        WorkingDirectory = location.TokensBeforeLast(@"\")
                    });
                }
                else
                {
                    QuickScriptWorker.ViewFileInNotepad(Host, location.Replace(".exe", ".cs"));
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
    }
}
