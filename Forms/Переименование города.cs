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
    public partial class Renaming_city : Form
    {
        string con = Connect.getConnect();
        public Renaming_city()
        {
            InitializeComponent();
        }
        public string id;

        private void Edit()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {

                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.EditCity'" + id + "','" + textBox1.Text + "'", connect);
                com.ExecuteNonQuery();

            }

        }

        private void Renaming_city_Load(object sender, EventArgs e)
        {
            City f = (City)this.Owner;
            id = f.ID_city.ToString();
            textBox1.Text = f.city.ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Edit();
            City f = (City)this.Owner;
            f.LoadAll();
        }
    }
}
