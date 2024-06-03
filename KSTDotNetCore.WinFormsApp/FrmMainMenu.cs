using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KSTDotNetCore.WinFormsApp
{
    public partial class FrmMainMenu : Form
    {
        public FrmMainMenu()
        {
            InitializeComponent();
        }

        private void newBlogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBlog frmBlog = new FrmBlog();
            frmBlog.ShowDialog();
        }

        private void blogToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmBlogList frmBlogList = new FrmBlogList();
            frmBlogList.ShowDialog();
        }
    }
}
