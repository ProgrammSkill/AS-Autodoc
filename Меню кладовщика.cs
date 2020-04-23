using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AS_Autodoc
{
    public partial class StorekeeperMenu : Form
    {
        string con = Connect.getConnect();
        public StorekeeperMenu()
        {
            InitializeComponent();
        }

        private void StorekeeperMenu_Load(object sender, EventArgs e)
        {

        }

        private void ОПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutTheProgram f = new AboutTheProgram();
            f.ShowDialog();
        }

        private void СменитьПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
