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
    public partial class Renaming_country : Form
    {
        string con = Connect.getConnect();
        public Renaming_country()
        {
            InitializeComponent();
        }
        public string id;

        private void Edit()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {

                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.EditCountry '" + id + "','" + textBox1.Text + "'", connect);
                com.ExecuteNonQuery();

            }

        }

        private void Renaming_country_Load(object sender, EventArgs e)
        {
            Country f = (Country)this.Owner;
            id = f.ID_country.ToString();
            textBox1.Text = f.country.ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                Edit();
            }
            else
            {
                MessageBox.Show("Поле пустое.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Country f = (Country)this.Owner;
            f.LoadAll();
        }
    }
}
