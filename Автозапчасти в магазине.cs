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
    public partial class AutoPartsInStore : Form
    {
        string con = Connect.getConnect();
        public AutoPartsInStore()
        {
            InitializeComponent();
        }

        private void AutoPartsInStore_Load(object sender, EventArgs e)
        {

        }
    }
}
