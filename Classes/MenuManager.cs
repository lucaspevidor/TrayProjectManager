using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            contextMenu.Items.Add("Add project");
            contextMenu.Items.Add("Add folder");            

            if (configManager.settings.RecentProjects.Length > 0)
            {
                contextMenu.Items.Add(new ToolStripSeparator());
                contextMenu.Items.Add(new ToolStripLabel("Recent"));
            }

            if (configManager.settings.WatchFolderPaths.Length > 0)
            {
                contextMenu.Items.Add(new ToolStripSeparator());
                contextMenu.Items.Add(new ToolStripLabel("Folders"));
            }
            
            if (configManager.settings.IndividualProjectPath.Length > 0)
            {
                contextMenu.Items.Add(new ToolStripSeparator());
                contextMenu.Items.Add(new ToolStripLabel("Projects"));
            }
            
            contextMenu.Items.Add(new ToolStripSeparator());
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
    }
}
