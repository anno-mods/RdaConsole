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
    public struct RdaCreatorOptions
    {
        public bool RecursiveFolders = true;
        public string RootFilepath = "";
        public FileHeader.Version Version;
    }

    internal class RdaCreator
    {
        private RDAWriter writer;
        private RDAFolder root;

        public RdaCreatorOptions Options { get; init; }

        public RdaCreator(RdaCreatorOptions options)
        { 
            Options = options;
            root = new RDAFolder(Options.Version);
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
            writer.Write(ExportFilename, Options.Version, compress, new RDAReader(), null);
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
                RDAFile? file = CreateRDAFile(Entry, Options.RootFilepath);
                if (file is not null)
                    files.Add(file);
            }
            else if (Directory.Exists(Entry))
            {
                RDAFolder? folder = CreateRDAFolder(Entry, Entry, Path.Combine(Options.RootFilepath, Entry), root);
                if (folder is not null)
                    root.AddFolder(folder);
            }
            root.AddFiles(files);
        }

        public RDAFolder? CreateRDAFolder(string FolderPath, string FolderName, string FolderFullPath, RDAFolder parent)
        {
            RDAFolder folder = new RDAFolder(Options.Version);

            folder.Name = FolderName;
            folder.FullPath = FolderFullPath;
            folder.Parent = parent;

            if (Options.RecursiveFolders)
            {
                foreach (var dir in Directory.EnumerateDirectories(FolderPath))
                {
                    RDAFolder? subfolder = CreateRDAFolder(dir, dir, Path.Combine(folder.FullPath, dir), folder);
                    if (subfolder is not null)
                        folder.AddFolder(subfolder);
                }
            }

            List<RDAFile> files = new();
            foreach (var file in Directory.GetFiles(FolderPath))
            {
                RDAFile? _rda = CreateRDAFile(file, folder.FullPath);
                if (_rda is not null)
                    files.Add(_rda);
            }
            folder.AddFiles(files);

            return folder;
        }

        public RDAFile? CreateRDAFile(string Filename, string Folder)
        {
            RDAFile? rdaFile = null;
            try
            {
                rdaFile = RDAFile.Create(Options.Version, Filename, Folder);
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not create File: {Filename}. Exception: {e.Message}");
            }
            return rdaFile;
        }
    }
}
