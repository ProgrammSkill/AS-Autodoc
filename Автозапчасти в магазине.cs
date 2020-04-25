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
    public partial class AutoPartsInStore : Form
    {
        string con = Connect.getConnect();
        public AutoPartsInStore()
        {
            InitializeComponent();
            SelectComboBox();
        }
        public string InsertOrEdit;
        public int ID_availability;
        public int ID_department;
        public string Title_auto_part;
        public string Manufacturer;
        public string Article;
        public string Sale_price;
        public int Amount;

        public void SelectComboBox()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT DISTINCT Sale.ID_department FROM Sale INNER JOIN Availability_auto_parts ON Sale.ID_department=Availability_auto_parts.ID_department", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        comboBox1.Items.Add(r[0].ToString());
                    }
                }
            }

            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Manufacturers", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
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
                SqlCommand com = new SqlCommand("EXECUTE dbo.SelectAvailability_auto_parts", connect);
                int i = 0;
                dataGridView1.Rows.Clear();
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1[0, i].Value = r[0].ToString();
                        dataGridView1[1, i].Value = r[1].ToString();
                        dataGridView1[2, i].Value = r[2].ToString();
                        dataGridView1[3, i].Value = r[3].ToString();
                        dataGridView1[4, i].Value = r[4].ToString();
                        dataGridView1[5, i].Value = r[5].ToString();
                        dataGridView1[6, i].Value = r[6].ToString();
                        dataGridView1[7, i].Value = r[7].ToString();
                        i++;
                    }
                }
            }
        }

        private void AutoPartsInStore_Load(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            InsertOrEdit = "Добавить";
            AddingAndEditingAvailabilityOfAutoParts f = new AddingAndEditingAvailabilityOfAutoParts();
            f.Owner = this;
            f.ShowDialog();
            f.ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                InsertOrEdit = "Редактировать";
                ID_availability = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
                ID_department = Convert.ToInt32(dataGridView1[1, dataGridView1.CurrentRow.Index].Value);
                Title_auto_part = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
                Manufacturer = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
                Article = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
                Sale_price = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();
                Amount = Convert.ToInt32(dataGridView1[6, dataGridView1.CurrentRow.Index].Value);
                AddingAndEditingAvailabilityOfAutoParts f = new AddingAndEditingAvailabilityOfAutoParts();
                f.Owner = this;
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана автозапчасть в магазине для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Delete()
        {
            string NumberStore= dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString().TrimEnd();
            DialogResult result = MessageBox.Show(
            "Вы точно хотите удалить автозапчасть в магазине № "+ NumberStore+"?",
            "Предупреждение",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button3);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection connect = new SqlConnection(con))
                {
                    connect.Open();
                    int id = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
                    SqlCommand com = new SqlCommand("DELETE FROM Availability_auto_parts WHERE ID_availability='" + id + "'", connect);
                    com.ExecuteNonQuery();

                }
            }
            this.TopMost = true;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Delete();
                LoadAll();
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void SearchByPriceRange()
        {
            if (textBox2.Text != "" & textBox3.Text != "")
            {
                using (SqlConnection connect = new SqlConnection(con))
                {
                    connect.Open();
                    string StartPrice = textBox2.Text.Replace(".", ",");
                    string EndPrice = textBox3.Text.Replace(".", ",");
                    SqlCommand com = new SqlCommand("EXECUTE dbo.SearchByPriceRange " + StartPrice + "," + EndPrice, connect);
                    int i = 0;
                    dataGridView1.Rows.Clear();
                    using (SqlDataReader r = com.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1[0, i].Value = r[0].ToString();
                            dataGridView1[1, i].Value = r[1].ToString();
                            dataGridView1[2, i].Value = r[2].ToString();
                            dataGridView1[3, i].Value = r[3].ToString();
                            dataGridView1[4, i].Value = r[4].ToString();
                            dataGridView1[5, i].Value = r[5].ToString();
                            dataGridView1[6, i].Value = r[6].ToString();
                            dataGridView1[7, i].Value = r[7].ToString();
                            i++;
                        }
                    }
                }
            }
            else if(textBox2.Text != "" & textBox3.Text == "")
            {
                using (SqlConnection connect = new SqlConnection(con))
                {
                    connect.Open();
                    string StartPrice = textBox2.Text.Replace(".", ",");
                    string EndPrice = "999999.00".ToString();
                    SqlCommand com = new SqlCommand("EXECUTE dbo.SearchByPriceRange " + StartPrice + "," + EndPrice, connect);
                    int i = 0;
                    dataGridView1.Rows.Clear();
                    using (SqlDataReader r = com.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1[0, i].Value = r[0].ToString();
                            dataGridView1[1, i].Value = r[1].ToString();
                            dataGridView1[2, i].Value = r[2].ToString();
                            dataGridView1[3, i].Value = r[3].ToString();
                            dataGridView1[4, i].Value = r[4].ToString();
                            dataGridView1[5, i].Value = r[5].ToString();
                            dataGridView1[6, i].Value = r[6].ToString();
                            dataGridView1[7, i].Value = r[7].ToString();
                            i++;
                        }
                    }
                }
            }
            else if (textBox2.Text == "" & textBox3.Text != "")
            {
                using (SqlConnection connect = new SqlConnection(con))
                {
                    connect.Open();
                    string StartPrice = "0.00".ToString();
                    string EndPrice = textBox3.Text.Replace(".", ",");
                    SqlCommand com = new SqlCommand("EXECUTE dbo.SearchByPriceRange " + StartPrice + "," + EndPrice, connect);
                    int i = 0;
                    dataGridView1.Rows.Clear();
                    using (SqlDataReader r = com.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1[0, i].Value = r[0].ToString();
                            dataGridView1[1, i].Value = r[1].ToString();
                            dataGridView1[2, i].Value = r[2].ToString();
                            dataGridView1[3, i].Value = r[3].ToString();
                            dataGridView1[4, i].Value = r[4].ToString();
                            dataGridView1[5, i].Value = r[5].ToString();
                            dataGridView1[6, i].Value = r[6].ToString();
                            dataGridView1[7, i].Value = r[7].ToString();
                            i++;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Visible = true;

                }
            }

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            SearchByPriceRange();
        }

        public void SearchByManufacturer()
        {
            if (comboBox2.Text != "" && comboBox2.Text != "Не указано")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string str = dataGridView1[3, i].Value.ToString();
                    int x = str.IndexOf(comboBox2.Text);
                    if (x > -1)
                    {
                        dataGridView1.Rows[i].Visible = true;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Visible = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Visible = true;
                }
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            SearchByManufacturer();
        }

        public void SearchByNameAutoParts()
        {
            if (textBox1.Text != "")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string str = dataGridView1[2, i].Value.ToString();
                    int x = str.IndexOf(textBox1.Text);
                    if (x > -1)
                    {
                        dataGridView1.Rows[i].Visible = true;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Visible = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Visible = true;

                }
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            SearchByNameAutoParts();
        }

        public void SearchByStoreNumber()
        {
            if (comboBox1.Text != "" && comboBox1.Text != "Не указано")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string str = dataGridView1[1, i].Value.ToString();
                    int x = str.IndexOf(comboBox1.Text);
                    if (x > -1)
                    {
                        dataGridView1.Rows[i].Visible = true;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Visible = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Visible = true;
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            SearchByStoreNumber();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void SearchByArticle()
        {
            if (textBox4.Text != "")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string str = dataGridView1[4, i].Value.ToString();
                    int x = str.IndexOf(textBox4.Text);
                    if (x > -1)
                    {
                        dataGridView1.Rows[i].Visible = true;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Visible = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Visible = true;

                }
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            SearchByArticle();
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
