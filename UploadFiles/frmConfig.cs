using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UploadFiles
{
    public partial class frmConfig : Form
    {
        public frmConfig()
        {
            InitializeComponent();
        }
        private void frmConfig_Load(object sender, EventArgs e)
        {
            this.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);

            txtSrcRootPath.Text = Env.copy_src_root;
            txtDstRootPath.Text = Env.copy_dst_root;

            txtServerURL.Text = Env.server_url;
            txtServerPort.Text = Env.server_port.ToString();
            txtServerUserName.Text = Env.server_user_name;
            txtServerPassword.Text = Env.server_password;

            rdoWeb.Checked = (Env.server_type == Env.SERVER_TYPE_WEB);
            rdoFTP.Checked = (Env.server_type == Env.SERVER_TYPE_FTP);

            chkPassive.Checked = Env.ftp_passive_mode;

            dateScheduleTime.Value = Env.schedule_time;

            chkCopyAllAtFirst.Checked = Env.copy_all_at_first;
            chkShowNextTime.Checked = Env.show_next_time;

            if (Env.windows_domain_name == "")
                this.Text = $"Configuration : {Env.windows_user_name}";
            else
                this.Text = $"Configuration : {Env.windows_domain_name}/{Env.windows_user_name}";

            rdoWeb_CheckedChanged(null, EventArgs.Empty);
        }

        private void btnBrowseSrc_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                txtSrcRootPath.Text = fbd.SelectedPath;
            }
        }

        private void btnBrowseDst_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                txtDstRootPath.Text = fbd.SelectedPath;
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            int port = 0;

            if (txtSrcRootPath.Text == "")
            {
                MessageBox.Show("Please input the source root directory path.");
                return;
            }
            if (txtDstRootPath.Text == "")
            {
                MessageBox.Show("Please input the destination root directory path.");
                return;
            }
            if (txtServerURL.Text == "")
            {
                MessageBox.Show("Please input the server URL.");
                return;
            }
            if (rdoFTP.Checked)
            {
                if (txtServerPort.Text == "")
                {
                    MessageBox.Show("Please input the server port.");
                    return;
                }
                if (!int.TryParse(txtServerPort.Text, out port))
                {
                    MessageBox.Show("Please input the valid server port.");
                    return;
                }
                if (txtServerUserName.Text == "")
                {
                    MessageBox.Show("Please input the server user name.");
                    return;
                }
            }

            Env.copy_src_root = txtSrcRootPath.Text;
            Env.copy_dst_root = txtDstRootPath.Text;

            Env.server_type = (rdoWeb.Checked) ? Env.SERVER_TYPE_WEB : Env.SERVER_TYPE_FTP;

            Env.ftp_passive_mode = chkPassive.Checked;

            Env.server_url = txtServerURL.Text;
            Env.server_port = port;
            Env.server_user_name = txtServerUserName.Text;
            Env.server_password = txtServerPassword.Text;

            Env.schedule_time = dateScheduleTime.Value;

            Env.copy_all_at_first = chkCopyAllAtFirst.Checked;
            Env.show_next_time = chkShowNextTime.Checked;

            if (!Env.save_into_ini())
            {
                MessageBox.Show("Failed to save INI.");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void rdoWeb_CheckedChanged(object sender, EventArgs e)
        {
            lblSep.Visible = !rdoWeb.Checked;
            txtServerPort.Visible = !rdoWeb.Checked;
            lblUserName.Visible = !rdoWeb.Checked;
            lblPassword.Visible = !rdoWeb.Checked;
            chkPassive.Visible = !rdoWeb.Checked;
            txtServerUserName.Visible = !rdoWeb.Checked;
            txtServerPassword.Visible = !rdoWeb.Checked;
        }
    }
}
