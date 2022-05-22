using RDAExplorer;

namespace RdaConsoleTool
{
    /// <summary>
    /// All unsafe untested. Lets see how this might break, kekw 
    /// </summary>
    public struct RdaCreatorOptions 
    {
        public bool RecursiveFolders = false;
        public string RootFilepath = "";
        public FileHeader.Version Version = FileHeader.Version.Version_2_2;

        public RdaCreatorOptions() { }
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

        public void SaveTo(string ExportFilename, bool compress = false, bool overwrite = false)
        {
            if (File.Exists(ExportFilename) && !overwrite) {
                Console.WriteLine($"Unable to save File {ExportFilename} because it already exists");
                return;
            }
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
                files.AddIfNotNull(file);
            }
            else if (Directory.Exists(Entry))
            {
                RDAFolder? folder = CreateRDAFolder(Path.Combine(Options.RootFilepath, Entry), root);
                root.AddFolderIfNotNull(folder);
            }
            root.AddFiles(files);
        }

        public RDAFolder? CreateRDAFolder(string FolderPath, RDAFolder parent)
        {
            RDAFolder folder = new RDAFolder(Options.Version);

            folder.Name = FolderPath;
            folder.FullPath = FolderPath;
            folder.Parent = parent;

            if (Options.RecursiveFolders)
            {
                foreach (var dir in Directory.EnumerateDirectories(FolderPath))
                {
                    RDAFolder? subfolder = CreateRDAFolder(dir, folder);
                    folder.AddFolderIfNotNull(subfolder);
                }
            }

            List<RDAFile> files = new();
            foreach (var file in Directory.GetFiles(FolderPath))
            {
                RDAFile? _rda = CreateRDAFile(file, folder.FullPath);
                files.AddIfNotNull(_rda);
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
