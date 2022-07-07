using RDAExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RdaConsoleTool
{
    internal class RdaUnpacker
    {
        private RDAReader _reader;

        public RdaUnpacker()
        { 
            _reader = new RDAReader();
        }

        public void UnpackFile(String Filename, String OutputFolderName, String filter, bool overwrite)
        {
            SetupReader(Filename);
            ExtractToOutput(OutputFolderName, filter, overwrite);
        }

        private void SetupReader(String Filename)
        {
            _reader.FileName = Filename;
            _reader.ReadRDAFile();
        }

        private void ExtractToOutput(String OutputFilename, String filter, bool overwrite)
        {
            if (Directory.Exists(OutputFilename) && !overwrite)
            {
                Console.WriteLine($"Output Directory already exists. Use -y to overwrite");
                return;
            }
            Directory.CreateDirectory(OutputFilename);
            var files = _reader.rdaFolder.GetAllFiles();
            var regex = new Regex(filter, RegexOptions.Compiled);
            files.RemoveAll(f => !regex.IsMatch(f.FileName));
            if (files.Count == 0)
            {
                Console.WriteLine($"Nothing left to extract, all files were filtered out");
                return;
            }

            _reader.ExtractFiles(files, OutputFilename);
        }
    }

    internal static class RDAReaderExtensions
    {
        public static void ExtractFiles(this RDAReader reader, List<RDAFile> files, String Path)
        {
            RDAExplorer.RDAFileExtension.ExtractAll(files, Path);
        }
    }
}
