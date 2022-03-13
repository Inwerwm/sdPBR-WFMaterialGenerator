using System;
using System.IO;
using System.Text;

namespace sdPBR_WFMaterialGenerator
{
    public class WFMaterialGenerator
    {
        private string SourceDirectory { get; }
        private string DestinationDirectory { get; }

        public WFMaterialGenerator(string sourceDirectory, string destinationDirectory)
        {
            if (!IsDirectory(sourceDirectory))
            {
                throw new ArgumentException("作成元材質フォルダとして指定されたフォルダが見つかりませんでした。");
            }

            if (!IsDirectory(destinationDirectory))
            {
                throw new ArgumentException("出力先材質フォルダとして指定されたフォルダが見つかりませんでした。");
            }

            SourceDirectory = Path.GetFullPath(sourceDirectory);
            DestinationDirectory = Path.GetFullPath(destinationDirectory);

        }

        public void Generate()
        {
            foreach (var (path, contents) in CreateMaterials(SourceDirectory))
            {
                WriteFile(path, contents);
            }
        }

        private IEnumerable<(string OutputPath, string Contents)> CreateMaterials(string path)
           => IsDirectory(path) ? GetChildItems(path).SelectMany(CreateMaterials)
            : IsEffectFile(path) ? Return(CreateWFMaterial(path))
            : Enumerable.Empty<(string OutputPath, string Contents)>();

        private (string OutputPath, string Contents) CreateWFMaterial(string path)
        {
            var sourceRelativePath = Path.GetRelativePath(SourceDirectory, path);
            var destinationPath = Path.Combine(DestinationDirectory, sourceRelativePath);

            return (destinationPath, GenerateMaterialString(path));
        }

        private static string GenerateMaterialString(string sourceFilePath) =>
            $"#define IN_THE_MIRROR{Environment.NewLine}" +
            $"#include \"{Path.GetFileName(sourceFilePath)}\"";

        private static void WriteFile(string outputPath, string contents)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            File.WriteAllText(outputPath, contents, Encoding.GetEncoding("shift_jis"));
        }

        private static bool IsDirectory(string path) => Directory.Exists(path);

        private bool IsEffectFile(string path) => Path.GetExtension(path).ToLower() == ".fx";

        private static IEnumerable<string> GetChildItems(string path)
        {
            return Directory.GetFiles(path).Concat(Directory.GetDirectories(path));
        }

        private static IEnumerable<T> Return<T>(T value)
        {
            yield return value;
        }
    }
}
