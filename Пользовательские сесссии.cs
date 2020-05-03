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
    public partial class UserSession : Form
    {
        string con = Connect.getConnect();
        public UserSession()
        {
            InitializeComponent();
        }

        public void LoadAll()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.SelectUserSession", connect);
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

        public void SearchByDateRange()
        {
            if (maskedTextBox1.Text != "  .  ." & maskedTextBox2.Text != "  .  .")
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

                            SqlCommand com = new SqlCommand("EXECUTE dbo.SelectUserSessionDate '" + StartDate + "','" + EndDate + "'", connect);
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
        }

        private void UserSession_Load(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            SearchByDateRange();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string str = dataGridView1[0, i].Value.ToString();
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

        private void Delete()
        {
            DialogResult result = MessageBox.Show(
            "Вы точно хотите удалить сессию пользователя?",
            "Предупреждение",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection connect = new SqlConnection(con))
                {
                    connect.Open();
                    string user = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
                    SqlCommand com = new SqlCommand("DELETE FROM InfoUsers WHERE Login_='" + user + "'", connect);
                    com.ExecuteNonQuery();

                }
            }
            this.TopMost = true;
        }

        private void Clear()
        {
            DialogResult result = MessageBox.Show(
            "Вы точно хотите удалить все сессии пользователей?",
            "Предупреждение",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection connect = new SqlConnection(con))
                {
                    connect.Open();
                    SqlCommand com = new SqlCommand("DELETE FROM UserSession ", connect);
                    com.ExecuteNonQuery();
                }
            }
            this.TopMost = true;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Delete();
                LoadAll();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Clear();
                LoadAll();
            }
        }
    }
}
