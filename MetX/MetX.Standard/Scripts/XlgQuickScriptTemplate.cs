using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.XPath;
using MetX.Standard.IO;
using MetX.Standard.Library;
using MetX.Standard.Library.Extensions;
using MetX.Standard.Library.Generics;
using MetX.Standard.Pipelines;

namespace MetX.Standard.Scripts
{
    [Serializable]
    public class XlgQuickScriptTemplate
    {
        public string Name { get; set; }
        public string TemplatePath { get; set; }

        public AssocArray<Asset> Assets = new();

        public XlgQuickScriptTemplate(string templatePath, string name = null)
        {
            TemplatePath = templatePath;
            Name = name ?? TemplatePath.LastPathToken();
            if (!Directory.Exists(TemplatePath)) return;

            ProcessPath(TemplatePath, TemplatePath);
        }

        private void ProcessPath(string originalPath, string path)
        {
            foreach (var file in Directory.GetFiles(path))
            {
                var assetName = file.LastPathToken();
                var asset = Assets[assetName].Item;
                asset.Template = File.ReadAllText(file);
                asset.OriginalAssetFilename = assetName;
                asset.RelativePath = path.LastToken(originalPath);
                if (asset.RelativePath.StartsWith(@"\"))
                    asset.RelativePath = asset.RelativePath.Substring(1);
            }

            foreach (var subfolder in Directory.GetDirectories(path))
            {
                ProcessPath(originalPath, subfolder);
            }
        }

        public ActualizationResult ActualizeCode(ActualizationSettings settings)
        {
            if (!settings.Simulate)
                Directory.CreateDirectory(settings.OutputFolder);
            var result = new ActualizationResult(settings);

            SetupAnswers(result);

            foreach (var asset in Assets)
            {
                var resolvedCode = asset.Item.ResolveVariables(result);

                if (resolvedCode.IsEmpty())
                {
                    result.ActualizeErrorText = $"{asset.Key} resolved to no code";
                    return result;
                }

                if (resolvedCode.Contains(Asset.LeftDelimiter) || resolvedCode.Contains(Asset.RightDelimiter))
                {
                    result.ActualizeErrorText = $"Not all variables in {asset.Key} file resolved";
                    return result;
                }

                var filePath = asset.Item.GetDestinationFilePath(settings);
                result.OutputFiles[asset.Item.RelativeFilePath].Value = resolvedCode;

                if (!settings.Simulate)
                    if (!FileSystem.SafeDelete(filePath))
                    {
                        result.ActualizeErrorText = $"Unable to write {asset.Key} to {filePath}";
                        return result;
                    }

                if (!settings.Simulate)
                {
                    var folder = filePath.TokensBeforeLast(@"\");
                    Directory.CreateDirectory(folder);
                    File.WriteAllText(filePath, resolvedCode);
                }
            }

            if (result.ActualizationSuccessful)
            {
                // Stage support files (MetX.*.dll & .pdb)
                var contents = FileSystem.DeepContents(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory));

                List<XlgFile> filesToCopy = new List<XlgFile>();

                contents.ForEachFile(func: file =>
                {
                    if (file.Extension.ToLower() is ".pdb" or ".dll" 
                        && file.Name.ToLower().Contains("metx.")) 
                        filesToCopy.Add(file);
                    return true;
                });

                foreach (var file in filesToCopy)
                {
                    file.CopyTo(result.Settings.OutputFolder);
                }

                var filename = settings.ProjectName.AsFilename(settings.ForExecutable ? ".exe" : ".dll");
                result.DestinationAssemblyFilePath = Path.Combine(settings.OutputFolder, "bin", "Debug", "net5.0-windows", filename);
            }

            return result;
        }

        private void SetupAnswers(ActualizationResult result)
        {
            var answers = result.Settings.Answers;

            answers["DestinationFilePath"].Value = result.Settings.Source.DestinationFilePath;
            answers["InputFilePath"].Value = result.Settings.Source.InputFilePath;
            answers["NameInstance"].Value = result.Settings.TemplateNameAsLegalFilenameWithoutExtension;
            answers["Project Name"].Value = result.Settings.TemplateNameAsLegalFilenameWithoutExtension;
            answers["UserName"].Value = Environment.UserName.LastToken(@"\").AsString("Unknown");
            answers["Guid Config"].Value = Guid.NewGuid().ToString("D");
            answers["Guid Project 1"].Value = Guid.NewGuid().ToString("D");
            answers["Guid Project 2"].Value = Guid.NewGuid().ToString("D");
            answers["Guid Solution"].Value = Guid.NewGuid().ToString("D");
            answers["Generated At"].Value = DateTime.Now.ToUniversalTime().ToString("s");

            var generationInstance = result.Settings.GeneratedAreas;
            generationInstance.ParseAndBuildAreas();

            foreach (var area in generationInstance)
                answers[area.Name].Value = area.ToString();
        }
    }
}