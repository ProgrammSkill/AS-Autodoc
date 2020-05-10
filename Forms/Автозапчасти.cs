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
    public partial class Autoparts : Form
    {
        string con = Connect.getConnect();
        string brand;
        public Autoparts()
        {
            InitializeComponent();
            SelectComboBox();

        }
        public string InsertOrEdit { get; set; }
        public int ID_autoparts { get; set; }
        public string Article { get; set; }
        public string Title { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string Comment { get; set; }

        public void SelectComboBox()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Brands", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        comboBox1.Items.Add(r[1].ToString());
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
                        comboBox3.Items.Add(r[1].ToString());
                    }
                }
            }

            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Country", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        comboBox4.Items.Add(r[1].ToString());
                    }
                }
            }
        }

        public void LoadAll()
        {
            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("dbo.SelectAutoparts", conn);
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
                        i++;
                    }
                }
            }

        }



        private void Autoparts_Load(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            InsertOrEdit = "Добавить";
            AddingAndEditingAutoParts f = new AddingAndEditingAutoParts();
            f.Owner = this;
            f.ShowDialog();
        }

        private void Delete()
        {
            DialogResult result = MessageBox.Show(
            "Вы точно хотите удалить автозапчасть?",
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
                    SqlCommand com = new SqlCommand("DELETE FROM Supply WHERE ID_autoparts='" + id + "'", connect);
                    com.ExecuteNonQuery();
                    com = new SqlCommand("DELETE FROM Autoparts WHERE ID_autoparts='" + id + "'", connect);
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

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                ID_autoparts = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
                Article = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
                Title = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
                Brand = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
                Model = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();
                Manufacturer = dataGridView1[6, dataGridView1.CurrentRow.Index].Value.ToString();
                Comment= dataGridView1[8, dataGridView1.CurrentRow.Index].Value.ToString();
                InsertOrEdit = "Редактировать";
                AddingAndEditingAutoParts f = new AddingAndEditingAutoParts();
                f.Owner = this;
                f.Show();
            }
            else
            {
                MessageBox.Show("Не выбрана автозапчасть для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string str = dataGridView1[1, i].Value.ToString();
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

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            brand = comboBox1.Text;
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("dbo.SelectAnotherModels'" + brand + "'", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        comboBox2.Items.Add(r[3].ToString());
                    }
                }
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string str = dataGridView1[2, i].Value.ToString();
                    int x = str.IndexOf(textBox2.Text);
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
            if (comboBox3.Text != "" && comboBox3.Text != "Не указано")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string str = dataGridView1[6, i].Value.ToString();
                    int x = str.IndexOf(comboBox3.Text);
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

        private void Button10_Click(object sender, EventArgs e)
        {
            if (comboBox4.Text != "" && comboBox4.Text != "Не указано")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string str = dataGridView1[7, i].Value.ToString();
                    int x = str.IndexOf(comboBox4.Text);
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
            if (comboBox1.Text != ""&&comboBox1.Text!="Не указано")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string str = dataGridView1[4, i].Value.ToString();
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

        private void Button8_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "" && comboBox2.Text != "Не указано")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string str = dataGridView1[5, i].Value.ToString();
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

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
