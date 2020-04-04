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
    public partial class Renaming__model : Form
    {
        string con = Connect.getConnect();
        public Renaming__model()
        {
            InitializeComponent();
        }
        public string id;

        private void Edit()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.EditModel '" + id + "','" + textBox1.Text + "'", connect);
                com.ExecuteNonQuery();
            }
        }

        private void Renaming__model_Load(object sender, EventArgs e)
        {
            Models f = (Models)this.Owner;
            id = f.ID_model.ToString();
            textBox1.Text = f.model.ToString();
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
            Models f = (Models)this.Owner;
            f.LoadAll();
        }
    }
}
