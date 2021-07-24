using System;
using System.IO;
using System.Text;

namespace sdPBR_WFMaterialGenerator
{
    public class WFMaterialGenerator
    {
        private string GenerateMaterialString(string targetFilePath) =>
            $"#define IN_THE_MIRROR{Environment.NewLine}" +
            $"#include \"{Path.GetFileName(targetFilePath)}\"";

        private void WriteFile(string outputFilename, string materialString)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            File.WriteAllText(outputFilename, materialString, Encoding.GetEncoding("shift_jis"));
        }

        public void GenerateWFMaterial(string targetFilePath)
        {
            var targetDir = Path.GetDirectoryName(targetFilePath);
            var targetFilename = Path.GetFileName(targetFilePath);

            WriteFile(Path.Combine(targetDir, "wf_" + targetFilename), GenerateMaterialString(targetFilePath));
        }
    }
}
