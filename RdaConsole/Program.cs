

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
                    creator = new RdaCreator(VersionFromInt(repack.Version));

                    creator.RecursiveFolders = repack.RecursiveFolderSearch;

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
