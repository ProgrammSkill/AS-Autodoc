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
using System.Globalization;

namespace AS_Autodoc
{
    public partial class Employee : Form
    {
        string con = Connect.getConnect();
        List<int> id_role;
        List<int> id_position;
        public Employee()
        {
            InitializeComponent();
            SelectComboBox();
        }

        public void SelectComboBox()
        {
            id_role = new List<int>();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Role_", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_role.Add(Convert.ToInt32(r[0]));
                        comboBox1.Items.Add(r[1].ToString());
                    }
                }
            }

            id_position = new List<int>();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Position", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_position.Add(Convert.ToInt32(r[0]));
                        comboBox2.Items.Add(r[1].ToString());
                    }
                }
            }
        }

        public void LoadAll()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                AccountAdministration f = (AccountAdministration)this.Owner;
                SqlCommand com = new SqlCommand("EXECUTE dbo.SelectInfoUsers '"+f.login+"'", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        textBox1.Text = r[0].ToString();
                        textBox2.Text = r[1].ToString();
                        comboBox1.Text = r[2].ToString();
                        textBox3.Text = r[3].ToString();
                        textBox4.Text = r[4].ToString();
                        textBox5.Text = r[5].ToString();
                        comboBox2.Text = r[6].ToString();
                        textBox6.Text = r[7].ToString();
                        maskedTextBox1.Text= r[8].ToString();
                        maskedTextBox2.Text = r[9].ToString();
                        maskedTextBox3.Text = r[10].ToString();
                    }
                }
            }
        }

        private void Edit()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                if(maskedTextBox3.Text!= "  .  .")
                {
                    SqlCommand com = new SqlCommand("EXECUTE dbo.EditInfoUsers '" + textBox1.Text + "','" + textBox2.Text + "','" +
                    id_role[comboBox1.SelectedIndex] + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" +
                    id_position[comboBox2.SelectedIndex] + "','" + textBox6.Text + "','" + maskedTextBox1.Text + "','" +
                    maskedTextBox2.Text + "','" + maskedTextBox3.Text + "'", connect);
                    com.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand com = new SqlCommand("EXECUTE dbo.EditInfoUsers '" + textBox1.Text + "','" + textBox2.Text + "','" +
                    id_role[comboBox1.SelectedIndex] + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" +
                    id_position[comboBox2.SelectedIndex] + "','" + textBox6.Text + "','" + maskedTextBox1.Text + "','" +
                    maskedTextBox2.Text + "'," + null, connect);
                    com.ExecuteNonQuery();
                }

            }
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            AccountAdministration f = (AccountAdministration)this.Owner;
            Edit();
            f.LoadAll();
        }

        private void MaskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void MaskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }
    }
}
