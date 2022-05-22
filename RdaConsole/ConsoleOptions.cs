
using CommandLine;

namespace RdaConsoleTool
{

    [Verb("rdapack", HelpText = "packs a list of directories and folders into a .rda file")]
    class RepackOptions
    {
        [Option('v', "version", Required = true, HelpText = "Compression Version: 1 for 2.0, 2 for 2.2")]
        public int Version { get; set; }

        [Option('f', "files", Required = true, HelpText = "List of files and directories. Can also be a whitespace")]
        public IEnumerable<string> Files { get; set; }

        [Option('r', "recursive", Required = false, HelpText = "Set to false if you do not want to search directories for files recursivly.")]
        public bool RecursiveFolderSearch { get; set; }

        [Option('o', "outputfile", Required = false, HelpText = "File name of the output file.")]
        public String OutputFilename { get; set; } = "output.rda";

        [Option('y', "overwrite", Required = false, HelpText = "Forces overwriting on the output file")]
        public bool Overwrite { get; set; } = false;
    }
}
