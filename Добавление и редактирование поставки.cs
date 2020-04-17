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
        List<int> id__manufacturer;
        List<int> id__article;
        //List<int> id_city;
        //List<int> id_street;
        //List<int> id_department;
        List<int> id_autoparts;
        public AddingAndEditingDelivery()
        {
            InitializeComponent();
            this.numericUpDown1.Minimum = 1;
            this.numericUpDown1.Maximum = 1000;
            SelectComboBox();
        }
        public int insertId;
        public int insertIdAvailability;
        public string id;

        //public string ID_availability;
        //public string ID_department;
        //public string ID_autoparts;
        //public string Price_holiday;
        //public string Amount;

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

            //id_city = new List<int>();
            //using (SqlConnection connect = new SqlConnection(con))
            //{
            //    connect.Open();
            //    SqlCommand com = new SqlCommand("EXECUTE dbo.SelectDepartmentCity", connect);
            //    using (SqlDataReader r = com.ExecuteReader())
            //    {
            //        while (r.Read())
            //        {
            //            id_city.Add(Convert.ToInt32(r[0]));
            //            comboBox2.Items.Add(r[1].ToString());
            //        }
            //    }
            //}

            //id_autoparts = new List<int>();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT DISTINCT Title FROM Autoparts", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        //id_autoparts.Add(Convert.ToInt32(r[0]));
                        comboBox2.Items.Add(r[0].ToString());
                    }
                }
            }
        }

        void MaxId()
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

        //void MaxIdAvailability()
        //{
        //    using (SqlConnection connection = new SqlConnection(con))
        //    {
        //        connection.Open();
        //        int id = 0;
        //        int n = 1;
        //        SqlCommand cm = new SqlCommand("SELECT * FROM Availability_auto_parts", connection);
        //        SqlDataReader r = cm.ExecuteReader();
        //        if (r.HasRows)
        //        {
        //            r.Close();
        //            cm = new SqlCommand("SELECT MAX(ID_availability) FROM Availability_auto_parts", connection);
        //            r = cm.ExecuteReader();
        //            while (r.Read())
        //            {
        //                id = Convert.ToInt32(r[0]) + 1;
        //                insertIdAvailability = id;
        //            }
        //        }
        //        else insertIdAvailability = n;
        //    }
        //}

        private void NewSupply()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.InsertSupply '" + insertId + "','" + id_supplier[comboBox1.SelectedIndex]+"','" + 
                id_autoparts[comboBox4.SelectedIndex] + "','"+textBox1.Text+"','"+numericUpDown1.Value+"','"+
                maskedTextBox1.Text+"','Новая'", connect);
                com.ExecuteNonQuery();
            }

            //int department = id_department[comboBox4.SelectedIndex];
            //int autopart = id_autoparts[comboBox5.SelectedIndex];
            //decimal price = Convert.ToDecimal(textBox1.Text.ToString().Replace(".", ","));
            //decimal amount = numericUpDown1.Value;

            //using (SqlConnection connect = new SqlConnection(con))
            //{
            //    SqlDataAdapter sda = new SqlDataAdapter("dbo.CheckAutoparts " + department + "," + autopart, connect);
            //    DataTable dt = new DataTable();
            //    sda.Fill(dt);
            //    if (dt.Rows[0][0].ToString() != "1")
            //    {
            //        connect.Open();
            //        SqlCommand com = new SqlCommand("EXECUTE dbo.InsertAvailability_auto_parts " + insertIdAvailability +
            //        "," + department + "," + autopart + "," + textBox1.Text + "," + amount, connect);
            //        com.ExecuteNonQuery();
            //    }
            //    else
            //    {
            //        connect.Open();
            //        SqlCommand com = new SqlCommand("SELECT * FROM Availability_auto_parts WHERE ID_department="+department+
            //        " AND ID_autoparts="+ autopart+"", connect);
            //        using (SqlDataReader r = com.ExecuteReader())
            //        {
            //            while (r.Read())
            //            {
            //                ID_availability = r[0].ToString();
            //                ID_department = r[1].ToString();
            //                ID_autoparts = r[2].ToString();
            //                Price_holiday = r[3].ToString();
            //                Amount = r[4].ToString();
            //            }
            //        }

            //        decimal p = Convert.ToDecimal(Price_holiday.ToString().Replace(".", ","));
            //        int a = Convert.ToInt32(Amount);
            //        decimal max;

            //        if (price > p)
            //        {
            //            max = price;
            //        }
            //        else
            //        {
            //            max = p;
            //        }
            //        string MaxStr = Convert.ToString(max).Replace(",",".");
            //        int newAmount = a + Convert.ToInt32(amount);

            //        connect.Close();
            //        connect.Open();
            //        SqlCommand EditAvailability = new SqlCommand("EXECUTE dbo.EditAvailability_auto_parts " + Convert.ToInt32(ID_availability) +
            //        "," + Convert.ToInt32(ID_department) + "," + Convert.ToInt32(ID_autoparts) + "," + MaxStr + "," + newAmount, connect);
            //        EditAvailability.ExecuteNonQuery();
            //    }
            //}
        }

        private void Edit()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.EditSupply '" + id + "','" + id_supplier[comboBox1.SelectedIndex] + "','" +
                id_autoparts[comboBox4.SelectedIndex] + "','" + textBox1.Text.ToString().Replace(",",".") + "','" + numericUpDown1.Value + "','" +
                maskedTextBox1.Text + "'", connect);
                com.ExecuteNonQuery();
            }
        }

        private void AddingAndEditingDelivery_Load(object sender, EventArgs e)
        {
            Supply f = (Supply)this.Owner;
            if (f.InsertOrEdit.ToString() == "Добавить")
            {
                this.Text = "Новая поставка";

                MaxId();
                //MaxIdAvailability();
            }
            else
            {
                this.Text = "Редактирование данных о поставке";
                id = f.id_supply.ToString();
                comboBox1.SelectedItem = f.supplier.ToString();
                comboBox2.SelectedItem = f.autopart.ToString();
                comboBox3.Text = f.manufacturer.ToString();
                comboBox4.Text = f.article.ToString();
                textBox1.Text = f.price_holiday.ToString().TrimEnd();
                numericUpDown1.Value = f.amount;
                maskedTextBox1.Text = f.delivery_date.ToString();
            }
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string manufacturer=comboBox2.Text;
            id__manufacturer = new List<int>();
            comboBox3.Items.Clear();
            comboBox3.Text = "";
            comboBox4.Items.Clear();
            comboBox4.Text = "";
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.SelectManufacturersAutoparts '"+manufacturer+"'", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id__manufacturer.Add(Convert.ToInt32(r[0]));
                        comboBox3.Items.Add(r[1].ToString());
                    }
                }
            }

            //string city = comboBox2.Text;
            //id_street = new List<int>();
            //comboBox3.Items.Clear();
            //comboBox3.Text = "";
            //comboBox4.Items.Clear();
            //comboBox4.Text = "";
            //using (SqlConnection connect = new SqlConnection(con))
            //{
            //    connect.Open();
            //    SqlCommand com = new SqlCommand("EXECUTE dbo.SelectDepartmentStreet '" + city + "'", connect);
            //    using (SqlDataReader r = com.ExecuteReader())
            //    {
            //        while (r.Read())
            //        {
            //            id_street.Add(Convert.ToInt32(r[0]));
            //            comboBox3.Items.Add(r[1].ToString());
            //        }
            //    }
            //}
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string autopart = comboBox2.Text;
            string manufacturer = comboBox3.Text;
            id_autoparts = new List<int>();
            comboBox4.Items.Clear();
            comboBox4.Text = "";
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.SelectArticleAutoparts '" + autopart + "','"+manufacturer+"'", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_autoparts.Add(Convert.ToInt32(r[0]));
                        comboBox4.Items.Add(r[1].ToString());
                    }
                }
            }

            //string street= comboBox3.Text;
            //id_department = new List<int>();
            //comboBox4.Items.Clear();
            //comboBox4.Text = "";
            //using (SqlConnection connect = new SqlConnection(con))
            //{
            //    connect.Open();
            //    SqlCommand com = new SqlCommand("EXECUTE dbo.SelectDepartmentHouse '" + street + "'", connect);
            //    using (SqlDataReader r = com.ExecuteReader())
            //    {
            //        while (r.Read())
            //        {
            //            id_department.Add(Convert.ToInt32(r[0]));
            //            comboBox4.Items.Add(r[1].ToString());
            //        }
            //    }
            //}
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
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
            Supply f = (Supply)this.Owner;
            if (f.InsertOrEdit.ToString() == "Добавить")
            {
                if (comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && comboBox4.Text != "" && textBox1.Text != "" && maskedTextBox1.Text != "")
                {
                    NewSupply();
                    MaxId();
                    f.LoadAll();
                }
                else
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && comboBox4.Text != "" && textBox1.Text != "" && maskedTextBox1.Text != "")
                {
                    Edit();
                    f.LoadAll();
                }
                else
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void ComboBox4_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
