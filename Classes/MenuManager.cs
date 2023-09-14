﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

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
                    contextMenu.Items.Add(project.Name, null,
                        (object s, EventArgs e) => OpenFolderWithCode(project)
                    );
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

                    foreach(string folder in folders)
                    {
                        string folderName = folder.Split('\\').Last();
                        if (folderName == null) { continue; }
                        ToolStripMenuItem subItem =
                            new(folderName, null, (object s, EventArgs e) => OpenFolderWithCode(
                                new FolderPath(folderName, folder)
                                ));
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
                    contextMenu.Items.Add(project.Name, null,
                        (object s, EventArgs e) => OpenFolderWithCode(project)
                    );
                }
            }

            contextMenu.Items.Add(new ToolStripLabel(" "));
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add("Settings", null, OpenSettings);
            contextMenu.Items.Add("Close", null, (object sender, EventArgs e) => Application.Exit());

            notifyIcon.ContextMenuStrip = contextMenu;
        }

        private void AddProject(object sender, EventArgs e)
        {
            FolderPicker picker = new(FolderPicker.DialogType.PROJECT);
            DialogResult res = picker.ShowDialog();

            if (res != DialogResult.OK)
                return;

            string name = picker.SelectedName;
            string path = picker.SelectedPath;

            configManager.settings.IndividualProjectPath = configManager.settings.
                IndividualProjectPath.Append(new FolderPath(name, path)).ToArray();

            configManager.GenerateConfig();
            PopulateMenus();
        }

        private void AddFolder(object sender, EventArgs e)
        {
            FolderPicker picker = new(FolderPicker.DialogType.FOLDER);
            DialogResult res = picker.ShowDialog();

            if (res != DialogResult.OK)
                return;

            string name = picker.SelectedName;
            string path = picker.SelectedPath;

            configManager.settings.WatchFolderPaths = configManager.settings.
                WatchFolderPaths.Append(new FolderPath(name, path)).ToArray();

            configManager.GenerateConfig();
            PopulateMenus();
        }

        private void OpenSettings(object sender, EventArgs e)
        {
            ConfigForm f = new ConfigForm(configManager);
            f.ShowDialog();
        }

        private void OpenFolderWithCode(FolderPath folderPath)
        {
            FolderPath[] recentProjects = configManager.settings.RecentProjects
                .Where(fp => fp.Path != folderPath.Path).Prepend(folderPath).Take(5).ToArray();
            configManager.settings.RecentProjects = recentProjects;
            configManager.GenerateConfig();

            Process.Start(configManager.settings.VSCodePath, folderPath.Path);
        }
    }
}
