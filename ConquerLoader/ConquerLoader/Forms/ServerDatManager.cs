using CLCore.Models;
using System;

namespace ConquerLoader.Forms
{
    public partial class ServerDatManager : MetroFramework.Forms.MetroForm
    {
        private LoaderConfig _LoaderConfig = null;
        public ServerDatManager()
        {
            InitializeComponent();
        }

        private void ServerDatManager_Load(object sender, EventArgs e)
        {
            lstServers.Items.Clear();
            lstGroups.Items.Clear();
            _LoaderConfig = Core.GetLoaderConfig();
            foreach (ServerConfiguration server in _LoaderConfig.Servers)
            {
                lstServers.Items.Add(server);
            }
        }

        private void LstServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstGroups.Items.Clear();
            System.Windows.Forms.ListBox lbox = (System.Windows.Forms.ListBox)sender;
            ServerDatGroup group = _LoaderConfig.Servers[lbox.SelectedIndex].Group;
            if (group != null)
            {
                lstGroups.Items.Add(group);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
