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
    public partial class RenamingСity : Form
    {
        string con = Connect.getConnect();
        public RenamingСity()
        {
            InitializeComponent();

            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(this.Width, this.Height);
            this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);
            this.StartPosition = FormStartPosition.CenterScreen;
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
            if (textBox1.Text != "")
            {
                Edit();
            }
            else
            {
                MessageBox.Show("Поле пустое.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            City f = (City)this.Owner;
            f.LoadAll();
        }
    }
}
