using RDAExplorer;

namespace RdaConsoleTool
{
    internal static class RDAFolderExtensions
    {
        public static void AddFolderIfNotNull(this RDAFolder folder, RDAFolder? toInject)
        {
            if (toInject is not null)
            {
                folder?.Folders.Add(toInject);
            }
        }

        public static bool IsEmpty(this RDAFolder folder)
        {
            try
            {
                return !(folder.Files.Any() && folder.Folders.Any());
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

    //dummy 

    internal static class RDAFileListExtensions
    {
        public static void AddIfNotNull(this List<RDAFile> files, RDAFile? toAdd)
        {
            if (toAdd is not null)
            {
                files.Add(toAdd!);
            }
        }

    }
}
