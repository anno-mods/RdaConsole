using RDAExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RdaConsoleTool
{
    /// <summary>
    /// All unsafe untested. Lets see how this might break, kekw
    /// </summary>
    internal class RdaCreator
    {
        private FileHeader.Version Version;

        public const string RootFilepath = "";

        private RDAWriter writer;
        private RDAFolder root;

        public bool RecursiveFolders = true;

        public RdaCreator(FileHeader.Version v)
        {
            Version = v;
            root = new RDAFolder(Version);
            writer = new RDAWriter(root);
        }

        public void AddToRDAStructure(IEnumerable<string> FileNames)
        {
            foreach (var FileName in FileNames)
            {
                AddToRDAStructure(FileName);
            }
        }

        public void SaveTo(string ExportFilename, bool compress = false)
        {
            writer.Write(ExportFilename, Version, compress, new RDAReader(), null);
        }

        /// <summary>
        /// Adds an Entry to an RDA folder
        /// </summary>
        /// <param name="Entry"></param>
        public void AddToRDAStructure(string Entry)
        {
            List<RDAFile> files = new();

            if (File.Exists(Entry))
            {
                RDAFile file = CreateRDAFile(Entry, RootFilepath);
                files.Add(file);
            }
            else if (Directory.Exists(Entry))
            {
                RDAFolder folder = CreateRDAFolder(Entry, Entry, Path.Combine(RootFilepath, Entry), root);
                root.AddFolder(folder);
            }
            root.AddFiles(files);
        }

        public RDAFolder? CreateRDAFolder(string FolderPath, string FolderName, string FolderFullPath, RDAFolder parent)
        {
            string generatedRDAFileName = RDAFile.FileNameToRDAFileName(FolderPath, RootFilepath);

            RDAFolder folder = new RDAFolder(Version);

            folder.Name = FolderName;
            folder.FullPath = FolderFullPath;
            folder.Parent = parent;

            if (RecursiveFolders)
            {
                foreach (var dir in Directory.EnumerateDirectories(FolderPath))
                {
                    var subfolder = CreateRDAFolder(dir, dir, Path.Combine(folder.FullPath, dir), folder);
                    folder.AddFolder(subfolder);
                }
            }

            List<RDAFile> files = new();
            foreach (var file in Directory.GetFiles(FolderPath))
            {
                files.Add(CreateRDAFile(file, folder.FullPath));
            }
            folder.AddFiles(files);

            return folder;
        }

        public RDAFile? CreateRDAFile(string Filename, string Folder)
        {
            var rdaFile = RDAFile.Create(Version, Filename, Folder);

            return rdaFile;
        }
    }
}
