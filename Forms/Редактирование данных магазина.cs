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
    public partial class EditingDepartment : Form
    {
        string con = Connect.getConnect();
        List<int> id_city;
        List<int> id_street;
        string id;
        public EditingDepartment()
        {
            InitializeComponent();

            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(this.Width, this.Height);
            this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);
            this.StartPosition = FormStartPosition.CenterScreen;

            id_city = new List<int>();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM City", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_city.Add(Convert.ToInt32(r[0]));
                        comboBox1.Items.Add(r[1].ToString());

                    }

                }

            }

            id_street = new List<int>();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Street", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_street.Add(Convert.ToInt32(r[0]));
                        comboBox2.Items.Add(r[1].ToString());

                    }

                }

            }
        }

        private void Edit()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.EditDepartment_store '" + id + "','"
                + id_city[comboBox1.SelectedIndex] + "','" + id_street[comboBox2.SelectedIndex] +
                "','" + textBox1.Text + "','" + maskedTextBox1.Text + "'", connect);
                com.ExecuteNonQuery();
            }
        }

        private void EditingDepartment_Load(object sender, EventArgs e)
        {
            DepartmentStore f = (DepartmentStore)this.Owner;
            id = f.id_department.ToString();
            comboBox1.SelectedItem = f.city.ToString();
            comboBox2.SelectedItem = f.street.ToString();
            textBox1.Text = f.house.ToString();
            maskedTextBox1.Text = f.telephone.ToString();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && comboBox2.Text != "" && textBox1.Text != "" && maskedTextBox1.Text != "")
            {
                Edit();
                DepartmentStore f = (DepartmentStore)this.Owner;
                f.LoadAll();
            }
            else
            {
                MessageBox.Show("Заполните все поля!.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
