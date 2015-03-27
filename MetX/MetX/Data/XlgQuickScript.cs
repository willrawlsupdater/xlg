using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using MetX.Library;
using Microsoft.CSharp;
using NArrange.ConsoleApplication;
using NArrange.Core;

namespace MetX.Data
{
    public class CaseFreeDictionary : Dictionary<string, string>
    {
        public new string this[string name]
        {
            get { return base[name.ToLower()]; }
            set { base[name.ToLower()] = value; }
        }
    }

    [Serializable]
    public class XlgQuickScriptTemplate
    {
        public string Name;
        public string TemplatePath;
        public CaseFreeDictionary Views = new CaseFreeDictionary();

        public XlgQuickScriptTemplate(string templatePath)
        {
            TemplatePath = templatePath;
            Name = TemplatePath.LastPathToken();
            if (Directory.Exists(TemplatePath))
            {
                foreach (string file in Directory.GetFiles(TemplatePath, "*.cs"))
                {
                    Views.Add(file.LastPathToken().ToLower().TokensBeforeLast(".cs"), File.ReadAllText(file));
                }
            }
        }
    }

    /// <summary>
    ///     Represents a clipboard processing script
    /// </summary>
    [Serializable]
    [XmlType(Namespace = "", AnonymousType = true)]
    public class XlgQuickScript
    {
        private static string m_IndependentTemplate;
        private static string m_Template;
        [XmlAttribute] public QuickScriptDestination Destination;
        [XmlAttribute] public string DestinationFilePath;
        [XmlAttribute] public string DiceAt;
        [XmlAttribute] public Guid Id;
        [XmlAttribute] public string Input;
        [XmlAttribute] public string InputFilePath;
        [XmlAttribute] public string Name;
        [XmlAttribute] public string Script;
        [XmlAttribute] public string SliceAt;
        [XmlAttribute] public string Template;
        public XlgQuickScript() { }

        public XlgQuickScript(string name, string script = null)
        {
            Name = name;
            Script = script ?? string.Empty;
            Id = Guid.NewGuid();
            Destination = QuickScriptDestination.Notepad;
            SliceAt = "End of line";
            DiceAt = "Space";
            Template = "Single file input";
        }

        public static CompilerResults CompileSource(string source, bool asExecutable)
        {
            CompilerParameters compilerParameters = new CompilerParameters
            {
                GenerateExecutable = asExecutable,
                GenerateInMemory = !asExecutable,
                IncludeDebugInformation = asExecutable
            };
            if (asExecutable)
            {
                compilerParameters.MainClass = "Processor.Program";
                compilerParameters.OutputAssembly =
                    Guid.NewGuid().ToString().Replace(new[] {"{", "}", "-"}, "").ToLower() + ".exe";
            }

            compilerParameters
                .ReferencedAssemblies
                .AddRange(
                    AppDomain.CurrentDomain
                             .GetAssemblies()
                             .Where(a => !a.IsDynamic)
                             .Select(a => a.Location)
                             .ToArray());
            CompilerResults compilerResults = new CSharpCodeProvider().CompileAssemblyFromSource(compilerParameters,
                source);
            return compilerResults;
        }

        public string ToCSharp(bool independent)
        {
            string code = new GenInstance(this, independent).CSharp;
            if (code.IsNullOrEmpty())
            {
                return code;
            }
            return FormatCSharpCode(code);
        }

        public static string FormatCSharpCode(string code)
        {
            TestLogger logger = new TestLogger();
            try
            {
                FileArranger fileArranger = new FileArranger(null, logger);
                string formattedCode = fileArranger.ArrangeSource(code);
                return formattedCode ?? code;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return code;
            }
        }

        public static bool Run(ILogger logger, CommandArguments commandArgs)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            if (commandArgs == null)
            {
                throw new ArgumentNullException("commandArgs");
            }
            bool flag;
            if (commandArgs.Restore)
            {
                logger.LogMessage(LogLevel.Verbose, "Restoring {0}...", (object) commandArgs.Input);
                string fileNameKey = BackupUtilities.CreateFileNameKey(commandArgs.Input);
                try
                {
                    flag = BackupUtilities.RestoreFiles(BackupUtilities.BackupRoot, fileNameKey);
                }
                catch (Exception ex)
                {
                    logger.LogMessage(LogLevel.Warning, ex.Message);
                    flag = false;
                }
                if (flag)
                {
                    logger.LogMessage(LogLevel.Info, "Restored");
                }
                else
                {
                    logger.LogMessage(LogLevel.Error, "Restore failed");
                }
            }
            else
            {
                flag = new FileArranger(commandArgs.Configuration, logger).Arrange(commandArgs.Input, commandArgs.Output, commandArgs.Backup);
                if (!flag)
                {
                    logger.LogMessage(LogLevel.Error, "Unable to arrange {0}.", (object) commandArgs.Input);
                }
                else
                {
                    logger.LogMessage(LogLevel.Info, "Arrange successful.");
                }
            }
            return flag;
        }

        public bool Load(string rawScript)
        {
            bool ret = false;
            SliceAt = "End of line";
            DiceAt = "Space";
            Template = "Single file input";

            if (String.IsNullOrEmpty(rawScript))
            {
                throw new ArgumentNullException("rawScript");
            }
            Name = rawScript.FirstToken(Environment.NewLine);
            if (String.IsNullOrEmpty(Name))
            {
                Name = "Unnamed " + Guid.NewGuid();
            }

            rawScript = rawScript.TokensAfterFirst(Environment.NewLine);
            if (!rawScript.Contains("~~QuickScript"))
            {
                Script = rawScript;
            }
            else
            {
                /*
                                if (rawScript.Contains("~~QuickScriptInput"))
                                {
                                    Input =
                                        rawScriptFromFile.TokensAfterFirst("~~QuickScriptInputStart:")
                                                 .TokensBeforeLast("~~QuickScriptInputEnd:");
                                    rawScript = rawScript.TokensAround("~~QuickScriptInputStart:",
                                        "~~QuickScriptInputEnd:" + Environment.NewLine);
                                }
                */

                StringBuilder sb = new StringBuilder();
                foreach (string line in rawScript.Lines())
                {
                    if (line.StartsWith("~~QuickScriptDefault:"))
                    {
                        ret = true;
                    }
                    else if (line.StartsWith("~~QuickScriptInput:"))
                    {
                        Input = line.TokensAfterFirst(":").Trim();
                        if (string.IsNullOrEmpty(Input))
                        {
                            Input = "Clipboard";
                        }
                    }
                    else if (line.StartsWith("~~QuickScriptDestination:"))
                    {
                        Destination = QuickScriptDestination.TextBox;
                        if (!Enum.TryParse(line.TokensAfterFirst(":"), out Destination))
                        {
                            Destination = QuickScriptDestination.TextBox;
                        }
                    }
                    else if (line.StartsWith("~~QuickScriptId:"))
                    {
                        if (!Guid.TryParse(line.TokensAfterFirst(":"), out Id))
                        {
                            Id = Guid.NewGuid();
                        }
                    }
                    else if (line.StartsWith("~~QuickScriptSliceAt:"))
                    {
                        SliceAt = line.TokensAfterFirst(":");
                    }
                    else if (line.StartsWith("~~QuickScriptTemplate:"))
                    {
                        Template = line.TokensAfterFirst(":");
                    }
                    else if (line.StartsWith("~~QuickScriptDiceAt:"))
                    {
                        DiceAt = line.TokensAfterFirst(":");
                    }
                    else if (line.StartsWith("~~QuickScriptInputFilePath:"))
                    {
                        InputFilePath = line.TokensAfterFirst(":");
                    }
                    else if (line.StartsWith("~~QuickScriptDestinationFilePath:"))
                    {
                        DestinationFilePath = line.TokensAfterFirst(":");
                    }
                    else
                    {
                        sb.AppendLine(line);
                    }
                }
                Script = sb.ToString();
            }

            return ret;
        }

        public string ToFileFormat(bool isDefault)
        {
            return "~~QuickScriptName:" + Name.AsString() + Environment.NewLine +
                   "~~QuickScriptInput:" + Input.AsString() + Environment.NewLine +
                   "~~QuickScriptDestination:" + Destination.AsString() + Environment.NewLine +
                   "~~QuickScriptId:" + Id.AsString() + Environment.NewLine +
                   "~~QuickScriptInputFilePath:" + InputFilePath + Environment.NewLine +
                   "~~QuickScriptDestinationFilePath:" + DestinationFilePath + Environment.NewLine +
                   "~~QuickScriptSliceAt:" + SliceAt + Environment.NewLine +
                   "~~QuickScriptDiceAt:" + DiceAt + Environment.NewLine +
                   "~~QuickScriptTemplate:" + Template + Environment.NewLine +
                   (isDefault
                       ? "~~QuickScriptDefault:" + Environment.NewLine
                       : String.Empty) +
                   Script.AsString();
        }

        public override string ToString() { return Name; }

        public static string ExpandScriptLineToSourceCode(string currScriptLine, int indent)
        {
            // backslash percent will translate to % after parsing
            currScriptLine = currScriptLine.Replace(@"\%", "~$~$").Replace("\"", "~#~$").Trim();

            bool keepGoing = true;
            int iterations = 0;
            while (keepGoing && ++iterations < 1000 && currScriptLine.Contains("%")
                   && currScriptLine.TokenCount("%") > 1)
            {
                string variableContent = currScriptLine.TokenAt(2, "%");
                string resolvedContent = String.Empty;
                if (variableContent.Length > 0)
                {
                    if (variableContent.Contains(" "))
                    {
                        string variableName = variableContent.FirstToken();
                        string variableIndex = variableContent.TokensAfterFirst();
                        int actualIndex;
                        if (int.TryParse(variableIndex, out actualIndex))
                        {
                            // Array index
                            resolvedContent = "\" + (" + variableName + ".Length <= " + variableIndex
                                              + " ? string.Empty : " + variableName + "[" + variableIndex + "]) + \"";
                        }
                        else
                        {
                            resolvedContent = "\" + " + variableName + "[\"" + variableIndex + "\"] + \"";
                        }
                    }
                    else
                    {
                        resolvedContent = "\" + " + variableContent + " + \"";
                    }
                }
                else
                {
                    keepGoing = false;
                }
                currScriptLine = currScriptLine.Replace("%" + variableContent + "%", resolvedContent);
            }
            currScriptLine = "Output.AppendLine(\"" + currScriptLine.Mid(3) + "\");";
            currScriptLine =
                currScriptLine.Replace("AppendLine(\" + ", "AppendLine(")
                              .Replace(" + \"\")", ")")
                              .Replace("Output.AppendLine(\"\")", "Output.AppendLine()");
            currScriptLine = (indent > 0
                ? new string(' ', indent + 12)
                : string.Empty) +
                             currScriptLine
                                 .Replace(" + \"\" + ", String.Empty)
                                 .Replace("\"\" + ", String.Empty)
                                 .Replace(" + \"\"", String.Empty)
                                 .Replace("~$~$", @"%")
                                 .Replace("~#~$", "\\\"");
            return currScriptLine;
        }

        public XlgQuickScript Clone(string name)
        {
            return new XlgQuickScript(name, Script)
            {
                Destination = Destination,
                DestinationFilePath = DestinationFilePath,
                DiceAt = DiceAt,
                Input = Input,
                InputFilePath = InputFilePath,
                SliceAt = SliceAt,
                Template = Template,
            };
        }
    }

    public class GenArea
    {
        public readonly List<string> Lines = new List<string>();
        public readonly string Name;
        public int Indent = 12;

        public GenArea(string name, int indent, string lines = null)
        {
            Name = name;
            Indent = indent;
            if (!string.IsNullOrEmpty(lines))
            {
                Lines = lines.LineList();
            }
        }

        public GenArea(string name)
        {
            Name = name;
            Indent = 12;
        }
    }

    public class GenInstance : List<GenArea>
    {
        public XlgQuickScriptTemplate Template;
        public string CSharp
        {
            get
            {
                string originalArea = string.Empty;
                foreach (string currScriptLine in m_Quick.Script.Lines())
                {
                    int indent = currScriptLine.Length - currScriptLine.Trim().Length;
                    if (currScriptLine.Contains("~~Start:") || currScriptLine.Contains("~~Begin:"))
                    {
                        SetArea("Start");
                    }
                    else if (currScriptLine.Contains("~~Finish:") || currScriptLine.Contains("~~Final:") || currScriptLine.Contains("~~End:"))
                    {
                        SetArea("Finish");
                    }
                    else if (currScriptLine.Contains("~~ReadInput:") || currScriptLine.Contains("~~Read:") || currScriptLine.Contains("~~Input:"))
                    {
                        SetArea("ReadInput");
                    }
                    else if (currScriptLine.Contains("~~ClassMember:") || currScriptLine.Contains("~~ClassMembers:") || currScriptLine.Contains("~~Fields:") || currScriptLine.Contains("~~Field:")
                             || currScriptLine.Contains("~~Members:") || currScriptLine.Contains("~~Member:"))
                    {
                        SetArea("ClassMembers");
                    }
                    else if (currScriptLine.Contains("~~ProcessLine:") || currScriptLine.Contains("~~ProcessLines:") || currScriptLine.Contains("~~Body:"))
                    {
                        SetArea("ProcessLine");
                    }
                    else if (currScriptLine.Contains("~~BeginString:"))
                    {
                        string stringName = currScriptLine.TokenAt(2, "~~BeginString:").Trim();
                        if (string.IsNullOrEmpty(stringName))
                        {
                            continue;
                        }
                        m_CurrArea.Lines.Add("string " + stringName + " = //~~String " + stringName + "~~//;");
                        originalArea = m_CurrArea.Name;
                        SetArea("String " + stringName);
                    }
                    else if (currScriptLine.Contains("~~EndString:"))
                    {
                        if (m_CurrArea.Lines.IsNullOrEmpty())
                        {
                            m_CurrArea.Lines.Add("string.Empty");
                        }
                        else if (!m_CurrArea.Lines.Any(x => x.Contains("\"")))
                        {
                            m_CurrArea.Lines[0] = "@\"" + m_CurrArea.Lines[0];
                            m_CurrArea.Lines[m_CurrArea.Lines.Count - 1] += "\"";
                        }
                        else if (!m_CurrArea.Lines.Any(x => x.Contains("`")))
                        {
                            m_CurrArea.Lines.TransformAllNotEmpty((line, index) => line.Replace("\"", "``"));
                            m_CurrArea.Lines[0] = "@\"" + m_CurrArea.Lines[0];
                            m_CurrArea.Lines[m_CurrArea.Lines.Count - 1] += "\".Replace(\"``\",\"\\\"\")";
                        }
                        else if (!m_CurrArea.Lines.Any(x => x.Contains("`")))
                        {
                            m_CurrArea.Lines.TransformAllNotEmpty((line, index) => "\t\"" + line.Replace("\"", "\\\"") + "\" + Enviornment.NewLine;" + Environment.NewLine);
                            m_CurrArea.Lines[0] = "@\"" + m_CurrArea.Lines[0];
                            m_CurrArea.Lines[m_CurrArea.Lines.Count - 1] += "\".Replace(\"``\",\"\\\"\")";
                        }
                        SetArea(originalArea);
                        originalArea = null;
                    }
                    else if (currScriptLine.Contains("~~:"))
                    {
                        if (m_CurrArea.Name == "ClassMembers")
                        {
                            m_CurrArea.Lines.Add(XlgQuickScript.ExpandScriptLineToSourceCode(currScriptLine, -1));
                        }
                        else
                        {
                            m_CurrArea.Lines.Add(XlgQuickScript.ExpandScriptLineToSourceCode(currScriptLine, indent));
                        }
                    }
                    else if (originalArea != null)
                    {
                        m_CurrArea.Lines.Add(currScriptLine);
                    }
                    else
                    {
                        m_CurrArea.Lines.Add((new string(' ', indent + m_CurrArea.Indent)) + currScriptLine);
                    }
                }

                StringBuilder sb = new StringBuilder(Template.Views[m_Independent ? "Exe" : "Native"]);
                foreach (GenArea area in this)
                {
                    sb.Replace("//~~" + area.Name + "~~//", String.Join(Environment.NewLine, area.Lines));
                }

                if (m_Independent)
                {
                    sb.Replace("//~~InputFilePath~~//", "\"" + m_Quick.InputFilePath.LastToken(@"\") + "\"");
                    sb.Replace("//~~Namespace~~//", (m_Quick.Name + "_" + DateTime.UtcNow.ToString("G") + "z").AsFilename());
                    sb.Replace("//~~NameInstance~~//", m_Quick.Name + " at " + DateTime.Now.ToString("G"));

                    switch (m_Quick.Destination)
                    {
                        case QuickScriptDestination.TextBox:
                        case QuickScriptDestination.Clipboard:
                        case QuickScriptDestination.Notepad:
                            sb.Replace("//~~DestinationFilePath~~//", "Path.GetTempFileName()");
                            break;

                        case QuickScriptDestination.File:
                            sb.Replace("//~~DestinationFilePath~~//", "\"" + m_Quick.DestinationFilePath.LastToken(@"\") + "\"");
                            break;
                    }
                }

                for (int i = 0; i < 10; i++)
                {
                    sb.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);
                }
                return sb.ToString();
            }
        }

        private readonly bool m_Independent;
        private readonly XlgQuickScript m_Quick;
        private GenArea m_CurrArea;

        public GenInstance(XlgQuickScript quick, bool independent)
        {
            m_Quick = quick;
            m_Independent = independent;

            m_CurrArea = new GenArea("ProcessLine");
            AddRange(new[]
            {
                m_CurrArea,
                new GenArea("ClassMembers", 8),
                new GenArea("Start"),
                new GenArea("Finish"),
                new GenArea("ReadInput"),
            });
        }

        private void SetArea(string areaName)
        {
            foreach (GenArea area in this.Where(area => area.Name == areaName))
            {
                m_CurrArea = area;
                return;
            }
            m_CurrArea = new GenArea(areaName);
            Add(m_CurrArea);
        }
    }
}