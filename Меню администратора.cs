using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AS_Autodoc
{
    public partial class AdminMenu : Form
    {
        public AdminMenu()
        {
            InitializeComponent();
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void AdminMenu_Load(object sender, EventArgs e)
        {

        }

        private void Enter_Click(object sender, EventArgs e)
        {
            AccountAdministration f = new AccountAdministration();
            f.ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            RegistrationOfUsers f = new RegistrationOfUsers();
            f.ShowDialog();
        }

        private void АдминистрированиеУчётныхЗаписейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountAdministration f = new AccountAdministration();
            f.ShowDialog();
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

        private void ВыходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Authorization_form f = (Authorization_form)this.Owner;
            f.Close();
        }
    }
}
