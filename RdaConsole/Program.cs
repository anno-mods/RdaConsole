

using CommandLine;
using RDAExplorer;

namespace RdaConsoleTool
{
    public class RdaTool
    {
        public static void Main(string[] args)
        {
            RdaCreator creator;

            Parser.Default.ParseArguments<RepackOptions>(args).MapResult(
                (repack) =>
                {
                    RdaCreatorOptions options = new RdaCreatorOptions();
                    options.Version = VersionFromInt(repack.Version);
                    options.RecursiveFolders = repack.RecursiveFolderSearch;

                    creator = new RdaCreator(options);

                    creator.AddToRDAStructure(repack.Files);
                    creator.SaveTo(repack.OutputFilename);

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
    }
}
