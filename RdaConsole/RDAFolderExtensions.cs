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
        public static void AddFolder(this RDAFolder folder, RDAFolder toInject)
        {
            folder?.Folders.Add(toInject);
        }
    }
}
