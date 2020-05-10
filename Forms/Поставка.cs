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
    public partial class Supply : Form
    {
        string con = Connect.getConnect();
        public Supply()
        {
            InitializeComponent();
            SelectComboBox();
        }
        public string InsertOrEdit { get; set; }

        public int id_supply;
        public string supplier;
        public int id_autopart;
        public string autopart;
        public string manufacturer;
        public string article;
        public string price_holiday;
        public int amount;
        public string sum;
        public string delivery_date;
        public string status;

        void SelectComboBox()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Suppliers", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        comboBox1.Items.Add(r[1].ToString());
                    }
                }
            }
        }

        public void LoadAll()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.SelectSuppply", connect);
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
                        dataGridView1[10, i].Value = r[10].ToString().TrimEnd();
                        i++;
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

        private void Supply_Load(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            InsertOrEdit = "Добавить";
            AddingAndEditingDelivery f = new AddingAndEditingDelivery();
            f.Owner = this;
            f.ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                if (dataGridView1[10, dataGridView1.CurrentRow.Index].Value.ToString() == "Новая")
                {
                    InsertOrEdit = "Редактировать";
                    id_supply = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
                    supplier = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
                    autopart = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
                    manufacturer = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
                    article = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();
                    price_holiday = dataGridView1[6, dataGridView1.CurrentRow.Index].Value.ToString();
                    amount = Convert.ToInt32(dataGridView1[7, dataGridView1.CurrentRow.Index].Value);
                    delivery_date = dataGridView1[9, dataGridView1.CurrentRow.Index].Value.ToString();

                    AddingAndEditingDelivery f = new AddingAndEditingDelivery();
                    f.Owner = this;
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Редактирование невозможно, поскольку данная поставка уже распределена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Не выбрана поставка для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Delete()
        {
            DialogResult result = MessageBox.Show(
            "Вы точно хотите удалить данную поставку?",
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
                    SqlCommand com = new SqlCommand("DELETE FROM Supply WHERE ID_supply='" + id+"'", connect);
                    com.ExecuteNonQuery();
                }
            }
            this.TopMost = true;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                if (dataGridView1[10, dataGridView1.CurrentRow.Index].Value.ToString() != "Распределена")
                {
                    Delete();
                    LoadAll();
                }
                else
                {
                    MessageBox.Show("Невозможно удалить поставку, поскольку данная поставка распределена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                if (dataGridView1[10, dataGridView1.CurrentRow.Index].Value.ToString() == "Новая")
                {
                    InsertOrEdit = "Редактировать";
                    id_supply = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
                    supplier = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
                    id_autopart = Convert.ToInt32(dataGridView1[2, dataGridView1.CurrentRow.Index].Value);
                    autopart = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
                    manufacturer = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
                    article = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();
                    price_holiday = dataGridView1[6, dataGridView1.CurrentRow.Index].Value.ToString();
                    amount = Convert.ToInt32(dataGridView1[7, dataGridView1.CurrentRow.Index].Value);
                    sum= dataGridView1[8, dataGridView1.CurrentRow.Index].Value.ToString();
                    delivery_date = dataGridView1[9, dataGridView1.CurrentRow.Index].Value.ToString();

                    Distribution f = new Distribution();
                    f.Owner = this;
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Распределение невозможно, поскольку данная поставка уже распределена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Не выбрана поставка для распределения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void SearchBySupplier()
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

        private void SearchByDate()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                string StartDate = maskedTextBox1.Text;
                string EndDate = maskedTextBox2.Text;
                if (TextIsDate(StartDate))
                {
                    if (TextIsDate(EndDate))
                    {
                        SqlCommand com = new SqlCommand("EXECUTE dbo.SearchForDeliveriesByDate '" + StartDate + "','" + EndDate + "'", connect);
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
                                dataGridView1[10, i].Value = r[10].ToString().TrimEnd();
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

        private void SearchForDeliveriesBySupplierAndDate()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                string StartDate = maskedTextBox1.Text;
                string EndDate = maskedTextBox2.Text;
                if (TextIsDate(StartDate))
                {
                    if (TextIsDate(EndDate))
                    {
                        SqlCommand com = new SqlCommand("EXECUTE dbo.SearchForDeliveriesBySupplierAndDate '" + comboBox1.Text + "','" + StartDate + "','" + EndDate + "'", connect);
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
                                dataGridView1[10, i].Value = r[10].ToString().TrimEnd();
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

        private void Button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && comboBox1.Text != "Не указано" && maskedTextBox1.Text == "  .  ." & maskedTextBox2.Text == "  .  .")
            {
                SearchBySupplier();
            }
            else if (comboBox1.Text == "" && comboBox1.Text == "Не указано" && maskedTextBox1.Text != "  .  ." & maskedTextBox2.Text != "  .  .")
            {
                SearchByDate();
            }
            else if (comboBox1.Text != "" && comboBox1.Text != "Не указано" && maskedTextBox1.Text != "  .  ." & maskedTextBox2.Text != "  .  .")
            {
                SearchForDeliveriesBySupplierAndDate();
            }
            else
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Visible = true;
                }
            }
        }
    }
}
