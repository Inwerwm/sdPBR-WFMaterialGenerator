using sdPBR_WFMaterialGenerator;
using System;
using System.IO;
using System.Linq;

namespace wfMaterialGenerator_Console
{
    class Program
    {
        private static WFMaterialGenerator Generator { get; } = new();
        private static uint GenerationCount { get; set; } = 0;

        static void Main(string[] args)
        {
            foreach (var arg in args)
            {
                Generate(arg);
            }

            Console.WriteLine($"完了{Environment.NewLine}{GenerationCount}個のファイルを生成しました。");
        }

        static void Generate(string path)
        {
            bool isEffectFile = Path.GetExtension(path).ToLower() == ".fx";
            bool isWFMaterial = Path.GetFileName(path).ToLower().StartsWith("wf_");

            if (IsFile(path) && isEffectFile && !isWFMaterial)
            {
                Generator.GenerateWFMaterial(path);
                GenerationCount++;
            }
            else if(IsDirectory(path))
            {
                foreach (var filePath in Directory.GetFiles(path).Concat(Directory.GetDirectories(path)))
                {
                    Generate(filePath);
                }
            }
        }

        static bool IsDirectory(string path) => Directory.Exists(path);
        static bool IsFile(string path) => File.Exists(path);
    }
}
