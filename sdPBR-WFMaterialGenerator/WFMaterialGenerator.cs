using System;
using System.IO;
using System.Text;

namespace sdPBR_WFMaterialGenerator
{
    public static class WFMaterialGenerator
    {
        private static void WriteFile(string outputPath, string materialString)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            File.WriteAllText(outputPath, materialString, Encoding.GetEncoding("shift_jis"));
        }

        private static string GenerateMaterialString(string sourceFilePath) =>
            $"#define IN_THE_MIRROR{Environment.NewLine}" +
            $"#include \"{Path.GetFileName(sourceFilePath)}\"";

        private static void GenerateWFMaterial(string sourceFilePath)
        {
            var targetDir = Path.GetDirectoryName(sourceFilePath);
            var targetFilename = "wf_" + Path.GetFileName(sourceFilePath);

            WriteFile(Path.Combine(targetDir, targetFilename), GenerateMaterialString(sourceFilePath));
        }
    }
}
