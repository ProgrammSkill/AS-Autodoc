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
    public partial class AddingAndEditingDelivery : Form
    {
        string con = Connect.getConnect();
        List<int> id_supplier;
        List<int> id_city;
        List<int> id_street;
        List<int> id_department;
        List<int> id_autoparts;
        public AddingAndEditingDelivery()
        {
            InitializeComponent();
            this.numericUpDown1.Minimum = 1;
            this.numericUpDown1.Maximum = 1000;
            SelectComboBox();
        }
        public int insertId;
        public string id;

        void SelectComboBox()
        {
            id_supplier = new List<int>();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Suppliers", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_supplier.Add(Convert.ToInt32(r[0]));
                        comboBox1.Items.Add(r[1].ToString());
                    }
                }
            }

            id_city = new List<int>();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.SelectDepartmentCity", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_city.Add(Convert.ToInt32(r[0]));
                        comboBox2.Items.Add(r[1].ToString());
                    }
                }
            }

            id_autoparts = new List<int>();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Autoparts", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_autoparts.Add(Convert.ToInt32(r[0]));
                        comboBox5.Items.Add(r[2].ToString());
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
                SqlCommand cm = new SqlCommand("SELECT * FROM Supply", connection);
                SqlDataReader r = cm.ExecuteReader();
                if (r.HasRows)
                {
                    r.Close();
                    cm = new SqlCommand("SELECT MAX(ID_supply) FROM Supply", connection);
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

        private void NewSupply()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.InsertSupply '" + insertId + "','" + id_supplier[comboBox1.SelectedIndex]+"','" + 
                id_autoparts[comboBox5.SelectedIndex] + "','"+textBox1.Text+"','"+numericUpDown1.Value+"','"+
                maskedTextBox1.Text+"'", connect);
                com.ExecuteNonQuery();
            }
                
            decimal a = 455.22m;
            //decimal b = Convert.ToDecimal(textBox1.Text.ToString());
            //string s = textBox1.Text;
            //string b = textBox1.Text+"m";
            //decimal c =Convert.ToDecimal(b);
        }

        private void AddingAndEditingDelivery_Load(object sender, EventArgs e)
        {
            Maxid();
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string city = comboBox2.Text;
            id_street = new List<int>();
            comboBox3.Items.Clear();
            comboBox3.Text = "";
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.SelectDepartmentStreet '" + city + "'", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_street.Add(Convert.ToInt32(r[0]));
                        comboBox3.Items.Add(r[1].ToString());
                    }
                }
            }
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string street= comboBox3.Text;
            id_department = new List<int>();
            comboBox4.Items.Clear();
            comboBox4.Text = "";
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.SelectDepartmentHouse '" + street + "'", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_department.Add(Convert.ToInt32(r[0]));
                        comboBox4.Items.Add(r[1].ToString());
                    }
                }
            }
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void MaskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            NewSupply();
            Maxid();
        }
    }
}
