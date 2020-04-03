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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void Enter_Click(object sender, EventArgs e)
        {
            Form f = new Suppliers();
            f.ShowDialog();
        }

        private void СтранаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Country f = new Country();
            f.ShowDialog();
        }

        private void ГородToolStripMenuItem_Click(object sender, EventArgs e)
        {
            City f = new City();
            f.ShowDialog();
        }

        private void УлицаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Street f = new Street();
            f.ShowDialog();
        }
    }
}
