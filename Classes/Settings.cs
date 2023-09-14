using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrayProjectManager.Classes
{
    public struct FolderPath
    {
        public string Name { get; }
        public string Path { get; }

        public FolderPath(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }

    public sealed class Settings
    {
        public FolderPath[] IndividualProjectPath { get; set; } = Array.Empty<FolderPath>();
        public FolderPath[] RecentProjects { get; set; } = Array.Empty<FolderPath>();
        public FolderPath[] WatchFolderPaths { get; set;} = Array.Empty<FolderPath>();
        public string VSCodePath { get; set; } = @"C:\Program Files\Microsoft VS Code\code.exe";
    }
}
