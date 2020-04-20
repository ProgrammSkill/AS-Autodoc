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
using System.Globalization;

namespace AS_Autodoc
{
    public partial class AddingAndEditingSales : Form
    {
        string con = Connect.getConnect();
        List<int> id_department;
        List<int> id_autoparts;
        public AddingAndEditingSales()
        {
            InitializeComponent();
            this.numericUpDown1.Minimum = 1;
            this.numericUpDown1.Maximum = 1000;
            SelectComboBox();
        }
        public int insertId;
        public string id;

        public string ID_availability;
        public string ID_department;
        public string ID_autoparts;
        public string Price_holiday;
        public string Amount;

        void SelectComboBox()
        {
            id_department = new List<int>();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Department_store", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_department.Add(Convert.ToInt32(r[0]));
                        comboBox1.Items.Add(r[0].ToString());
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
                SqlCommand com = new SqlCommand("SELECT * FROM Sale", connection);
                SqlDataReader r = com.ExecuteReader();
                if (r.HasRows)
                {
                    r.Close();
                    com = new SqlCommand("SELECT MAX(ID_sale) FROM Sale", connection);
                    r = com.ExecuteReader();
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
                int departament = id_department[comboBox1.SelectedIndex];
                int autopart = id_autoparts[comboBox4.SelectedIndex];
                SqlCommand com = new SqlCommand("SELECT * FROM Availability_auto_parts WHERE ID_department=" + departament +
                " AND ID_autoparts=" + autopart + "", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        ID_availability = r[0].ToString();
                        ID_department = r[1].ToString();
                        ID_autoparts = r[2].ToString();
                        Price_holiday = r[3].ToString();
                        Amount = r[4].ToString();
                    }
                }

                int amount = Convert.ToInt32(Amount);
                int saleAmount = Convert.ToInt32(numericUpDown1.Value);
                if (saleAmount <= amount)
                {
                    com = new SqlCommand("EXECUTE dbo.InsertSale '" + insertId + "','" + id_department[comboBox1.SelectedIndex] + "','" +
                    id_autoparts[comboBox4.SelectedIndex] + "','" + numericUpDown1.Value + "','" + maskedTextBox1.Text + "'", connect);
                    com.ExecuteNonQuery();

                    int newAmount = amount - saleAmount;
                    SqlCommand EditAvailability = new SqlCommand("EXECUTE dbo.EditAvailability_auto_parts " + Convert.ToInt32(ID_availability) +
                    "," + id_department[comboBox1.SelectedIndex] + "," + id_autoparts[comboBox4.SelectedIndex] + "," + Price_holiday.Replace(",",".") + "," + newAmount, connect);
                    EditAvailability.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("В отделе магазина недостаточно количество  данной автозапчасти для продажи." +
                    " Укажите количество для продажи автозапчасти, которое не превышает количество в отделе магазина", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void NewSale_Load(object sender, EventArgs e)
        {
            MaxId();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string manufacturer = comboBox2.Text;
            comboBox3.Items.Clear();
            comboBox3.Text = "";
            comboBox4.Items.Clear();
            comboBox4.Text = "";
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.SelectManufacturersAutoparts '" + manufacturer + "'", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        comboBox3.Items.Add(r[1].ToString());
                    }
                }
            }
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
                SqlCommand com = new SqlCommand("EXECUTE dbo.SelectArticleAutoparts '" + autopart + "','" + manufacturer + "'", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_autoparts.Add(Convert.ToInt32(r[0]));
                        comboBox4.Items.Add(r[1].ToString());
                    }
                }
            }
        }

        static bool TextIsDate(string text)
        {
            var dateFormat = "dd.MM.yyyy";
            DateTime scheduleDate;
            if (DateTime.TryParseExact(text, dateFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out scheduleDate))
            {
                return true;
            }
            return false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Sale f = (Sale)this.Owner;
            //if (f.InsertOrEdit.ToString() == "Добавить")
            //{
                if (comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && comboBox4.Text != "" && maskedTextBox1.Text != "")
                {
                    if (TextIsDate(maskedTextBox1.Text))
                    {
                        Insertion();
                        MaxId();
                    }
                    else
                    {
                        MessageBox.Show("Введён неправельный формат даты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            //}
            //else
            //{
            //    if (comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && comboBox4.Text != "" && textBox1.Text != "" && maskedTextBox1.Text != "")
            //    {
            //        Edit();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            f.LoadAll();
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string number_store = comboBox1.Text;
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            comboBox3.Items.Clear();
            comboBox3.Text = "";
            comboBox4.Items.Clear();
            comboBox4.Text = "";
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.SelectComboBoxAutoparts "+number_store, connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        comboBox2.Items.Add(r[0].ToString());
                    }
                }
            }
        }
    }
}
