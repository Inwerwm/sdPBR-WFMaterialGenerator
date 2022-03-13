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

        private static bool IsDirectory(string path) => Directory.Exists(path);
        private static bool IsFile(string path) => File.Exists(path);

        public static void Generate(string sourceMaterialDirectory, string destinationMaterialDirectory)
        {

        }
    }
}
