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
    public partial class EditingBrandAndModel : Form
    {
        string con = Connect.getConnect();
        List<int> id_brand;
        List<int> id_model;
        public EditingBrandAndModel()
        {
            InitializeComponent();

            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(this.Width, this.Height);
            this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);
            this.StartPosition = FormStartPosition.CenterScreen;

            id_brand = new List<int>();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Brands", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_brand.Add(Convert.ToInt32(r[0]));
                        comboBox1.Items.Add(r[1].ToString());
                    }
                }
            }

            id_model = new List<int>();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Models", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_model.Add(Convert.ToInt32(r[0]));
                        comboBox2.Items.Add(r[1].ToString());
                    }
                }
            }
        }
        public string id;

        private void Edit()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.EditBrand_and_model '" + id + "','" +
                id_brand[comboBox1.SelectedIndex] + "','" + id_model[comboBox2.SelectedIndex] + "'", connect);
                com.ExecuteNonQuery();
            }
        }

        private void EditingBrandAndModel_Load(object sender, EventArgs e)
        {
            BrandsAndModels f = (BrandsAndModels)this.Owner;
            id = f.ID_brd_mdl.ToString();
            comboBox1.SelectedItem = f.Title_brand.ToString();
            comboBox2.SelectedItem = f.Title_model.ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text != "" && comboBox2.Text != "")
            {
                Edit();
                BrandsAndModels f = (BrandsAndModels)this.Owner;
                f.LoadAll();
            }
            else
            {
                MessageBox.Show("Заполните все поля!.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
