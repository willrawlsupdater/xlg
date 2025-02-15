﻿using System;
using System.Diagnostics;
using System.IO;
using MetX.Standard.Interfaces;
using MetX.Standard.Pipelines;
using MetX.Windows.WinApi;

namespace MetX.Windows.Library
{
    public class QuickScriptWorker
    {
        public static Exception ViewTextInNotepad(IGenerationHost host, string source, bool isCSharpCode)
        {
            try
            {
                var tempFile = Path.Combine(Path.GetTempPath(),
                    $"qscript{Guid.NewGuid().ToString().Substring(1, 6)}{(isCSharpCode ? ".cs" : ".txt")}");
                File.WriteAllText(tempFile, source);
                Process.Start("notepad", tempFile);
            }
            catch (Exception ex)
            {
                return ex;
            }

            return null;
        }

        public static void ViewFileInNotepad(IGenerationHost host, string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) return;
                var notepadProcess = Process.Start("notepad", filePath);
                ActiveWindow.Move(notepadProcess);
            }
            catch (Exception ex)
            {
                host.MessageBox.Show(ex.ToString());
            }
        }

        public const string FirstScript = @"
if(line.Length < 20)
	Output.AppendLine(line);
else
	Output.AppendLine(line.Substring(0, 20));
";

        public static readonly string ExampleTutorialScript = $@"
~~Using:
using System.Diagnostics;

~~Members:
Dictionary<string, string> d = new Dictionary<string, string>();

~~Start:
// Write a header
~~:Lines starting with ~~: Are shorthand for Output.AppendLine(...) with special expansion
~~:This makes it easier to write when encoding lines of C#, VB, java, xml, etc.
~~:Example: Line # (First word): Line content
Output.AppendLine(""Or if you prefer, you can simply write C# code"");{Environment.NewLine}d[""previous""] = ""Ready ""; // sets the ""Previous"" dictionary item to ""Ready ""{Environment.NewLine}
~~ProcessLine:
// These lines are called for every non blank line in the source/clipboard
// Several variables are always defined including:
//   line is the string content of the current line
//   number is the current line number being processed
//   lineCount is the total number of lines to be processed.
//   d is a Dictionary<string, string> that persists across lines. Use it as you want
//   sb is a StringBuilder that you will write your output to (which goes in output/clipboard)

string[] word = line.Split(' ');
if(word.Length > 0) d[""previous""] += word[0] + "", "";{Environment.NewLine}

// The following two lines are equivalent
// Output.AppendLine(""\""Example\"":\t\"" + number + \"" (\"" + word[0] + ""): \"""" + line + ""\"""");{Environment.NewLine}~~:""Example"":\t%number% (%word 0%): ""%line%""

~~Finish:
// After the last line
~~:
~~:This is only written at the end
~~:
~~:""Previous"" dictionary entry is written out: {Environment.NewLine}~~:%d previous%{Environment.NewLine}";
    }
}