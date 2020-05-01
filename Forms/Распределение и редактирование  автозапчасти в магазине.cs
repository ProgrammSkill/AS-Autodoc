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
    public partial class AddingAndEditingAvailabilityOfAutoParts : Form
    {
        string con = Connect.getConnect();
        List<int> id_department;
        List<int> id_autoparts;
        public AddingAndEditingAvailabilityOfAutoParts()
        {
            InitializeComponent();
            this.numericUpDown1.Minimum = 1;
            this.numericUpDown1.Maximum = 1000;
            SelectComboBox();
        }
        public int insertId;
        public string id;

        void MaxId()
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                int id = 0;
                int n = 1;
                SqlCommand com = new SqlCommand("SELECT * FROM Availability_auto_parts", connection);
                SqlDataReader r = com.ExecuteReader();
                if (r.HasRows)
                {
                    r.Close();
                    com = new SqlCommand("SELECT MAX(ID_availability) FROM Availability_auto_parts", connection);
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

            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT DISTINCT Title FROM Autoparts", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        comboBox2.Items.Add(r[0].ToString());
                    }
                }
            }
        }

        private void Insertion()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM Availability_auto_parts WHERE ID_department= " + id_department[comboBox1.SelectedIndex] + " AND ID_autoparts="+id_autoparts[comboBox4.SelectedIndex], connect);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() != "1")
                {
                    SqlCommand com = new SqlCommand("EXECUTE dbo.InsertAvailability_auto_parts " + insertId +
                    "," + id_department[comboBox1.SelectedIndex] + "," + id_autoparts[comboBox4.SelectedIndex] + "," + textBox1.Text.ToString().Replace(",", ".") + "," + numericUpDown1.Value, connect);
                    com.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Данная автозапчасть уже имеется в магазине с номером "+id_department[comboBox1.SelectedIndex].ToString().TrimEnd(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Edit()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                AutoPartsInStore f = (AutoPartsInStore)this.Owner;
                if (f.ID_department.ToString() != comboBox1.Text&f.Title_auto_part!=comboBox2.Text&
                    f.Manufacturer!=comboBox3.Text&f.Article!=comboBox4.Text)
                {
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM Availability_auto_parts WHERE ID_department= " + id_department[comboBox1.SelectedIndex] + " AND ID_autoparts=" + id_autoparts[comboBox4.SelectedIndex], connect);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() != "1")
                    {
                        SqlCommand com = new SqlCommand("EXECUTE dbo.EditAvailability_auto_parts " + id +
                        "," + id_department[comboBox1.SelectedIndex] + "," + id_autoparts[comboBox4.SelectedIndex] + "," + textBox1.Text.ToString().Replace(",", ".") + "," + numericUpDown1.Value, connect);
                        com.ExecuteNonQuery();
                    }
                    else
                    {
                        MessageBox.Show("Данная автозапчасть уже имеется в магазине с номером " + id_department[comboBox1.SelectedIndex].ToString().TrimEnd(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    SqlCommand com = new SqlCommand("EXECUTE dbo.EditAvailability_auto_parts " + id +
                    "," + id_department[comboBox1.SelectedIndex] + "," + id_autoparts[comboBox4.SelectedIndex] + "," + textBox1.Text.ToString().Replace(",", ".") + "," + numericUpDown1.Value, connect);
                    com.ExecuteNonQuery();
                }
            }
        }

        private void AddingAndEditingAvailabilityOfAutoParts_Load(object sender, EventArgs e)
        {
            AutoPartsInStore f = (AutoPartsInStore)this.Owner;
            if (f.InsertOrEdit.ToString() == "Добавить")
            {
                this.Text = "Распределение автозапчасти";
                MaxId();
            }
            else
            {
                this.Text = "Редактирование распредделения автозапчасти";
                id = f.ID_availability.ToString();
                comboBox1.SelectedItem = f.ID_department.ToString();
                comboBox2.SelectedItem = f.Title_auto_part.ToString();
                comboBox3.Text = f.Manufacturer.ToString();
                comboBox4.Text = f.Article.ToString();
                textBox1.Text = f.Sale_price.ToString();
                if (f.Amount > 0)
                {
                    numericUpDown1.Value = f.Amount;
                }
                else
                {
                    this.numericUpDown1.Minimum = 0;
                    numericUpDown1.Value = 0;
                }
            }            
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string autopart = comboBox2.Text;
            comboBox3.Items.Clear();
            comboBox3.Text = "";
            comboBox4.Items.Clear();
            comboBox4.Text = "";
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.SelectManufacturersAutoparts '" + autopart + "'", connect);
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

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            AutoPartsInStore f = (AutoPartsInStore)this.Owner;
            if (comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && comboBox4.Text != "" && textBox1.Text != "")
            {
                if (f.InsertOrEdit.ToString() == "Добавить")
                {
                    Insertion();
                    MaxId();
                }
                else
                {
                    Edit();
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            f.LoadAll();
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            int length = textBox1.Text.Length;
            if (length == 0 && (ch == ',' || ch == '.'))
            {
                e.Handled = true;
            }
            if (!Char.IsDigit(ch) && ch != 8 && ((ch != ',' || textBox1.Text.Contains(",")) && (ch != '.' || textBox1.Text.Contains("."))))
            {
                e.Handled = true;
            }
        }
    }
}
