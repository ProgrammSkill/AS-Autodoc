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
    public partial class AddingSupplier : Form
    {
        string con = Connect.getConnect();
        int insertId;
        List<int> id_country;
        List<int> id_city;
        List<int> id_street;
        public AddingSupplier()
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


        void Maxid()
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

        string Check()
        {
            Suppliers f = (Suppliers)this.Owner;
            if(f.InsertOrEdit=="Добавление")
            {
                return "Добавление";
            }
            else
            {
                return "Редактирование";
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
                textBox6.Text+"','"+textBox7.Text+"'", connect);
                com.ExecuteNonQuery();
            }

        }

        private void AddingSupplier_Load(object sender, EventArgs e)
        {
            AddingSupplier f = new AddingSupplier();
            Suppliers f1 = new Suppliers();
            //if(Check()=="Добавить")
            //{
            //    f.Text = "Добавленпаппапие поставщика";
            //}
            //else
            //{
            //    f.Text = "Редактирование поставщика";
            //}
            if(f1.InsertOrEdit=="Добавить")
            {
                f.Text = "INSERT";
            }
            else
            {
                f.Text = "edit";
            }
            textBox2.Text = f1.InsertOrEdit;
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Insertion();
            Maxid();
            Suppliers f = (Suppliers)this.Owner;
            f.LoadAll();
        }

        private void TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
