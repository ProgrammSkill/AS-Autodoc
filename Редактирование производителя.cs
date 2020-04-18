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
    public partial class EditingManufacturer : Form
    {
        string con = Connect.getConnect();
        List<int> id_country;
        public EditingManufacturer()
        {
            InitializeComponent();

            id_country = new List<int>();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Country", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_country.Add(Convert.ToInt32(r[0]));
                        comboBox1.Items.Add(r[1].ToString());
                    }
                }
            }
        }
        public string  id;

        private void Edit()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.EditManufacturer '" + id + "','" +textBox1.Text+"','"+ id_country[comboBox1.SelectedIndex] + "'", connect);
                com.ExecuteNonQuery();
            }
        }

        private void RenamingManufacturer_Load(object sender, EventArgs e)
        {
            Manufacturers f = (Manufacturers)this.Owner;
            id = f.ID_manufacturer.ToString();
            textBox1.Text = f.manufacturer.ToString();
            comboBox1.SelectedItem = f.country.ToString();
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != ""&&comboBox1.Text!="")
            {
                Edit();
                Manufacturers f = (Manufacturers)this.Owner;
                f.LoadAll();
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
