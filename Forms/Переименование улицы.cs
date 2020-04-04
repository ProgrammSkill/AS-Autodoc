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
    public partial class Renaming__street : Form
    {
        string con = Connect.getConnect();
        public Renaming__street()
        {
            InitializeComponent();
        }
        public string id;

        private void Edit()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.EditStreet '" + id + "','" + textBox1.Text + "'", connect);
                com.ExecuteNonQuery();
            }
        }

        private void Renaming__street_Load(object sender, EventArgs e)
        {
            Street f = (Street)this.Owner;
            id = f.ID_street.ToString();
            textBox1.Text = f.street.ToString();
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
            Street f = (Street)this.Owner;
            f.LoadAll();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
