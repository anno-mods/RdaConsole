using RDAExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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

        public void UnpackFile(String Filename, String OutputFolderName, bool overwrite)
        {
            SetupReader(Filename);
            ExtractToOutput(OutputFolderName, overwrite);
        }

        private void SetupReader(String Filename)
        {
            _reader.FileName = Filename;
            _reader.ReadRDAFile();
        }

        private void ExtractToOutput(String OutputFilename, bool overwrite)
        {
            if (Directory.Exists(OutputFilename) && !overwrite)
            {
                Console.WriteLine($"Output Directory already exists. Use -y to overwrite");
                return;
            }
            Directory.CreateDirectory(OutputFilename);
            _reader.ExtractAllFiles(OutputFilename);
        }
    }

    internal static class RDAReaderExtensions
    {
        public static void ExtractAllFiles(this RDAReader reader, String Path)
        {
            RDAExplorer.RDAFileExtension.ExtractAll(reader.rdaFolder.GetAllFiles(), Path);
        }
    }
}
