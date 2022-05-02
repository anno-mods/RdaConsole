﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}