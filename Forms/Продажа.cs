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
    public partial class Sale : Form
    {
        string con = Connect.getConnect();
        public Sale()
        {
            InitializeComponent();
            SelectComboBox();
        }
        public string InsertOrEdit { get; set; }

        public int ID_sale;
        public int ID_department;
        public int ID_auto_part;
        public string Title_auto_part;
        public string Manufacturer;
        public string Article;
        public int Amount;
        public string Date_of_sale;

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
        }

        public void LoadAll()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.SelectSale", connect);
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
                        dataGridView1[8, i].Value = r[8].ToString();
                        dataGridView1[9, i].Value = r[9].ToString().Remove(10);
                        i++;
                    }
                }
            }
        }

        private void Delete()
        {
            string title_country = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            DialogResult result = MessageBox.Show(
            "Вы точно хотите удалить продажу из списка?",
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
                    SqlCommand com = new SqlCommand("DELETE FROM Sale WHERE ID_sale='" + id + "'", connect);
                    com.ExecuteNonQuery();
                }
            }
            this.TopMost = true;
        }

        private void Sale_Load(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            InsertOrEdit = "Добавить";
            AddingAndEditingSales f = new AddingAndEditingSales();
            f.Owner = this;
            f.ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                InsertOrEdit = "Редактировать";
                ID_sale = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
                ID_department = Convert.ToInt32(dataGridView1[1, dataGridView1.CurrentRow.Index].Value);
                ID_auto_part = Convert.ToInt32(dataGridView1[2, dataGridView1.CurrentRow.Index].Value);
                Title_auto_part = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
                Manufacturer = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
                Article = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();
                Amount = Convert.ToInt32(dataGridView1[7, dataGridView1.CurrentRow.Index].Value);
                Date_of_sale = dataGridView1[9, dataGridView1.CurrentRow.Index].Value.ToString();

                AddingAndEditingSales f = new AddingAndEditingSales();
                f.Owner = this;
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана поставка для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Delete();
                LoadAll();
            }
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

        public void SearchForAutoParts()
        {
            if (textBox1.Text != "")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string str = dataGridView1[3, i].Value.ToString();
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

        public void SearchByDateRange()
        {
            if (maskedTextBox1.Text != "" & maskedTextBox2.Text != "")
            {
                using (SqlConnection connect = new SqlConnection(con))
                {
                    connect.Open();
                    string StartDate = maskedTextBox1.Text;
                    string EndDate = maskedTextBox2.Text;
                    if (TextIsDate(maskedTextBox1.Text))
                    {
                        if (TextIsDate(maskedTextBox2.Text))
                        {

                            SqlCommand com = new SqlCommand("EXECUTE dbo.SearchDateSale '" + StartDate + "','" + EndDate + "'", connect);
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
                                    dataGridView1[8, i].Value = r[8].ToString();
                                    dataGridView1[9, i].Value = r[9].ToString().Remove(10);
                                    i++;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Введён неправельный формат даты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            maskedTextBox2.Clear();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введён неправельный формат даты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        maskedTextBox1.Clear();
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


        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

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

        private void Button4_Click(object sender, EventArgs e)
        {
            SearchByStoreNumber();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            SearchByDateRange();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            SearchForAutoParts();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
