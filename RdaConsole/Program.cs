

using CommandLine;
using RDAExplorer;

namespace RdaConsoleTool
{
    public class RdaTool
    {
        public static void Main(string[] args)
        {
            RdaCreator creator;

            Parser.Default.ParseArguments<RepackOptions, ExtractOptions>(args).MapResult(
                (RepackOptions repack) =>
                {
                    RdaCreatorOptions options = new RdaCreatorOptions();
                    options.Version = VersionFromInt(repack.Version);
                    options.RecursiveFolders = repack.RecursiveFolderSearch;

                    creator = new RdaCreator(options);

                    if (repack.ForceNoOut) DisableRdaExplorerConsole();

                    creator.AddToRDAStructure(repack.Files);
                    creator.SaveTo(repack.OutputFilename, overwrite: repack.Overwrite);

                    return 0;
                },
                (ExtractOptions extract) =>
                {
                    RdaUnpacker unpacker = new RdaUnpacker();

                    if (extract.ForceNoOut) DisableRdaExplorerConsole();

                    foreach (String file in extract.Files)
                    {
                        unpacker.UnpackFile(file, extract.OutputFolderName, extract.Overwrite);
                    }
                    return 0;
                },
                e => 1
            );
        }

        private static FileHeader.Version VersionFromInt(int i)
        {
            if (i == 1) return FileHeader.Version.Version_2_0;
            else return FileHeader.Version.Version_2_2;
        }

        private static void DisableRdaExplorerConsole()
        {
            UISettings.EnableConsole = false;
        }
    }
}
