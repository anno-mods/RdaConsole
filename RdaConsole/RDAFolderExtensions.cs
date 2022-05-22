using RDAExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

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
