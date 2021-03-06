﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace AS_Autodoc
{
    public partial class AddingAndEditingSupplier : Form
    {
        string con = Connect.getConnect();
        int insertId;
        List<int> id_country;
        List<int> id_city;
        List<int> id_street;
        public AddingAndEditingSupplier()
        {
            InitializeComponent();

            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(this.Width, this.Height);
            this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(this.Width, this.Height);
            this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);
            this.StartPosition = FormStartPosition.CenterScreen;

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
                        comboBox2.Items.Add(r[1].ToString());
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
                        comboBox3.Items.Add(r[1].ToString());

                    }

                }

            }
        }
        string id;


        void MaxId()
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                int id = 0;
                int n = 1;
                SqlCommand cm = new SqlCommand("SELECT * FROM Suppliers", connection);
                SqlDataReader r = cm.ExecuteReader();
                if (r.HasRows)
                {
                    r.Close();
                    cm = new SqlCommand("SELECT MAX(ID_supplier) FROM Suppliers", connection);
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
                SqlCommand com = new SqlCommand("EXECUTE dbo.InsertSupplier '" + insertId + "','" + textBox1.Text + "','" + textBox2.Text +
                "','" + textBox3.Text + "','" + textBox4.Text + "','" + id_country[comboBox1.SelectedIndex] +
                "','" + id_city[comboBox2.SelectedIndex] + "','" + id_street[comboBox3.SelectedIndex] + "','" + textBox5.Text+"','"+
                maskedTextBox1.Text+"','"+textBox6.Text+"'", connect);
                com.ExecuteNonQuery();
            }
        }

        public void Edit()
        {
            string con = Connect.getConnect();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.EditSupplier '" + id + "','" + textBox1.Text + "','" + textBox2.Text +
                "','" + textBox3.Text + "','" + textBox4.Text + "','" + id_country[comboBox1.SelectedIndex] +
                "','" + id_city[comboBox2.SelectedIndex] + "','" + id_street[comboBox3.SelectedIndex] + "','" + textBox5.Text + "','" +
                maskedTextBox1.Text + "','" + textBox6.Text + "'", connect);
                com.ExecuteNonQuery();
            }
        }

        private void AddingSupplier_Load(object sender, EventArgs e)
        {
            Suppliers f = (Suppliers)this.Owner;
            if (f.InsertOrEdit.ToString()=="Добавить")
            {
               this.Text = "Добавление нового поставщика";
                MaxId();        
            }
            else
            {
                this.Text = "Редактирование данных поставщика";
                id = f.ID_supplier.ToString();
                textBox1.Text = f.Title.ToString();
                textBox2.Text = f.TIN.ToString();
                textBox3.Text = f.CIO.ToString();
                textBox4.Text = f.FIO_director.ToString();
                comboBox1.SelectedItem = f.Country.ToString();
                comboBox2.SelectedItem = f.City.ToString();
                comboBox3.SelectedItem = f.Street.ToString();
                textBox5.Text = f.House.ToString();
                maskedTextBox1.Text = f.Telephone.ToString();
                textBox6.Text = f.Email.ToString();
            }
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        bool isValid(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" &&
               comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && textBox5.Text != "" &&
               maskedTextBox1.Text != "" && textBox6.Text != "")
            {
                if (isValid(textBox6.Text) == true)
                {
                    Suppliers f = (Suppliers)this.Owner;
                    if (f.InsertOrEdit.ToString() == "Добавить")
                    {
                        Insertion();
                        MaxId();
                        f.LoadAll();
                    }
                    else
                    {
                        Edit();
                        f.LoadAll();
                    }
                }
                else
                {
                    MessageBox.Show("Введён неправильный формат адреса электронной почты (email)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void MaskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
