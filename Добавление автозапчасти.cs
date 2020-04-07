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

    public partial class Adding_auto_parts : Form
    {
        string con = Connect.getConnect();
        List<int> id_brd_mdl;
        List<int> id_brand;
        List<int> id_manufacturer;
        string brand;
        public Adding_auto_parts()
        {
            InitializeComponent();
            SelectComboBox();
        }
        public int insertId;

        void SelectComboBox()
        {
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

            id_manufacturer = new List<int>();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Manufacturers", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_manufacturer.Add(Convert.ToInt32(r[0]));
                        comboBox3.Items.Add(r[1].ToString());
                    }
                }
            }
        }

        void Maxid()
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                int id = 0;
                int n = 1;
                SqlCommand cm = new SqlCommand("SELECT * FROM Autoparts", connection);
                SqlDataReader r = cm.ExecuteReader();
                if (r.HasRows)
                {
                    r.Close();
                    cm = new SqlCommand("SELECT MAX(ID_autoparts) FROM Autoparts", connection);
                    r = cm.ExecuteReader();
                    while (r.Read())
                    {
                        id = Convert.ToInt32(r[0]) + 1;
                        insertId = id;
                    }
                }
                else insertId = n;

            }
        }

        private void Insertion()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.InsertAutopart '" + insertId + "','" + textBox1.Text + "','" +
                textBox2.Text+"','"+id_brd_mdl[comboBox2.SelectedIndex] + "','"+id_manufacturer[comboBox3.SelectedIndex]+
                "','"+textBox3.Text+"'", connect);
                com.ExecuteNonQuery();
            }
            textBox1.Clear();
        }

        private void Adding_auto_parts_Load(object sender, EventArgs e)
        {
            //using (SqlConnection connect = new SqlConnection(con))
            //{
            //    connect.Open();
            //    SqlCommand com = new SqlCommand("SELECT Title_brand FROM Brands", connect);
            //    using (SqlDataReader r = com.ExecuteReader())
            //    {
            //        while (r.Read())
            //        {
            //            comboBox1.Text = r[0].ToString();
            //        }
            //    }
            //brand = comboBox1.Text;
            //textBox1.Text = brand;

            //}
        }


        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            brand = comboBox1.Text;
            id_brd_mdl = new List<int>();
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("dbo.SelectAnotherModels'" + brand + "'", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_brd_mdl.Add(Convert.ToInt32(r[0]));
                        comboBox2.Items.Add(r[3].ToString());
                    }
                }
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && textBox3.Text != "")
            {
                using (SqlConnection connect = new SqlConnection(con))
                {
                    connect.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM Autoparts WHERE Title= '" + textBox1.Text + "'", connect);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() != "1")
                    {
                        Insertion();
                    }
                    else
                    {
                        MessageBox.Show("Данный артикул занят другим товаром", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                Maxid();
                Autoparts f = (Autoparts)this.Owner;
                f.LoadAll();
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


