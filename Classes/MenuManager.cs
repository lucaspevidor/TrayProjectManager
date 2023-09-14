using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;

namespace TrayProjectManager.Classes
{

    class MenuManager
    {
        ConfigManager configManager;
        NotifyIcon notifyIcon;

        public MenuManager(ConfigManager configManager, NotifyIcon notifyIcon)
        {
            this.configManager = configManager;
            this.notifyIcon = notifyIcon;

            this.PopulateMenus();
        }

        void PopulateMenus()
        {
            ContextMenuStrip contextMenu = new();
            contextMenu.Items.Add("Add project", null, AddProject);
            contextMenu.Items.Add("Add folder", null, AddFolder);

            FolderPath[] recentProjects = configManager.settings.RecentProjects;
            FolderPath[] folderList = configManager.settings.WatchFolderPaths;
            FolderPath[] projectList = configManager.settings.IndividualProjectPath;

            if (configManager.settings.RecentProjects.Length > 0)
            {
                contextMenu.Items.Add(new ToolStripLabel(" "));
                contextMenu.Items.Add(new ToolStripLabel("Recent"));
                contextMenu.Items.Add(new ToolStripSeparator());

                foreach(FolderPath project in  recentProjects)
                {
                    FolderPath fp = new(project.Name, project.Path, FolderPathType.SUBFOLDER);
                    ToolStripMenuItem item = new(fp.Name);                    
                    item.MouseUp += (s, e) => HandleMenuItemClick(fp, e);
                    contextMenu.Items.Add(item);
                }
            }

            if (configManager.settings.WatchFolderPaths.Length > 0)
            {
                contextMenu.Items.Add(new ToolStripLabel(" "));
                contextMenu.Items.Add(new ToolStripLabel("Folders"));
                contextMenu.Items.Add(new ToolStripSeparator());

                foreach(FolderPath folderPath in folderList) 
                {
                    string[] folders = Directory.GetDirectories(folderPath.Path);

                    ToolStripMenuItem mainItem = new(folderPath.Name);
                    mainItem.MouseUp += (s, e) => HandleMenuItemClick(folderPath, e);

                    foreach(string folder in folders)
                    {
                        string folderName = folder.Split('\\').Last();
                        if (folderName == null) { continue; }
                        ToolStripMenuItem subItem = new(folderName);
                        subItem.MouseUp += (s, e) => HandleMenuItemClick(
                                new FolderPath(folderName, folder, FolderPathType.SUBFOLDER), e);
                        mainItem.DropDownItems.Add(subItem);
                    }

                    contextMenu.Items.Add(mainItem);
                }
            }
            
            if (configManager.settings.IndividualProjectPath.Length > 0)
            {
                contextMenu.Items.Add(new ToolStripLabel(" "));
                contextMenu.Items.Add(new ToolStripLabel("Projects"));                
                contextMenu.Items.Add(new ToolStripSeparator());

                foreach(FolderPath project in projectList)
                {
                    ToolStripMenuItem item = new(project.Name);
                    item.MouseUp += (s, e) => HandleMenuItemClick(project, e);
                    contextMenu.Items.Add(item);
                }
            }

            contextMenu.Items.Add(new ToolStripLabel(" "));
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add("Settings", null, OpenSettings);
            contextMenu.Items.Add("Close", null, (s, e) => Application.Exit());

            notifyIcon.ContextMenuStrip = contextMenu;
        }

        private void AddProject(object? sender, EventArgs e)
        {
            FolderPicker picker = new(FolderPicker.DialogType.PROJECT);
            picker.StartPosition = FormStartPosition.CenterScreen;
            DialogResult res = picker.ShowDialog();

            if (res != DialogResult.OK)
                return;

            string name = picker.SelectedName;
            string path = picker.SelectedPath;

            var existingItem = configManager.settings.IndividualProjectPath.Where(f => f.Path == path);
            if (existingItem.Any())
            {
                MessageBox.Show("Project already added: " + existingItem.First().Name,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            configManager.settings.IndividualProjectPath = configManager.settings.
                IndividualProjectPath.Append(new FolderPath(name, path, FolderPathType.PROJECT)).ToArray();

            configManager.GenerateConfig();
            PopulateMenus();
        }

        private void AddFolder(object? sender, EventArgs e)
        {
            FolderPicker picker = new(FolderPicker.DialogType.FOLDER);
            picker.StartPosition = FormStartPosition.CenterScreen;
            DialogResult res = picker.ShowDialog();

            if (res != DialogResult.OK)
                return;

            string name = picker.SelectedName;
            string path = picker.SelectedPath;

            var existingItem = configManager.settings.WatchFolderPaths.Where(f => f.Path == path);
            if (existingItem.Any())
            {
                MessageBox.Show("Folder already added: " + existingItem.First().Name,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            configManager.settings.WatchFolderPaths = configManager.settings.
                WatchFolderPaths.Append(new FolderPath(name, path, FolderPathType.FOLDER)).ToArray();

            configManager.GenerateConfig();
            PopulateMenus();
        }

        private void OpenSettings(object sender, EventArgs e)
        {
            ConfigForm f = new ConfigForm(configManager);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
        }

        private void HandleMenuItemClick(FolderPath folderPath, MouseEventArgs e)
        {               
            if (e.Button == MouseButtons.Left && folderPath.Type != FolderPathType.FOLDER)
            {
                OpenFolderWithCode(folderPath);
            }
            if (e.Button == MouseButtons.Right && folderPath.Type != FolderPathType.SUBFOLDER) 
            {
                RemoveItem(folderPath);
            }
        }

        private void OpenFolderWithCode(FolderPath folderPath)
        {
            if (!File.Exists(configManager.settings.VSCodePath))
            {
                MessageBox.Show("VSCode not found. Please check the application settings",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FolderPath[] recentProjects = configManager.settings.RecentProjects
                .Where(fp => fp.Path != folderPath.Path).Prepend(folderPath).Take(5).ToArray();
            configManager.settings.RecentProjects = recentProjects;
            configManager.GenerateConfig();
            PopulateMenus();

            Process.Start(configManager.settings.VSCodePath, folderPath.Path);
        }

        private void RemoveItem(FolderPath folderPath)
        {
            DialogResult res = MessageBox.Show("Remove " + folderPath.Name + "?",
                "Remove item", MessageBoxButtons.YesNo);
            if (res == DialogResult.No)
                return;

            if (folderPath.Type == FolderPathType.FOLDER)
            {
                configManager.settings.WatchFolderPaths =
                    configManager.settings.WatchFolderPaths.Where(f => f.Path != folderPath.Path).ToArray();
            }
            else if (folderPath.Type == FolderPathType.PROJECT)
            {
                configManager.settings.IndividualProjectPath =
                    configManager.settings.IndividualProjectPath.Where(f => f.Path != folderPath.Path).ToArray();
            }

            configManager.GenerateConfig();
            PopulateMenus();
        }
    }
}
