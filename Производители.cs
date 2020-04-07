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
    public partial class Manufacturers : Form
    {
        string con = Connect.getConnect();
        List<int> id_country;
        public Manufacturers()
        {
            InitializeComponent();

            id_country= new List<int>();
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
        }
        public int insertId;
        public int ID_manufacturer;
        public string manufacturer;
        public string country;

        public void LoadAll()
        {
            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("dbo.SelectManufacturers", conn);
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
                        i++;
                    }
                }
            }

        }

        private void Delete()
        {
            string title_country = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            DialogResult result = MessageBox.Show(
            "Вы точно хотите удалить проиизводителя из списка?",
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
                    SqlCommand com = new SqlCommand("DELETE FROM Manufacturers WHERE ID_manufacturer='" + id + "'", connect);
                    com.ExecuteNonQuery();
                }
            }
            this.TopMost = true;
        }

        void Maxid()
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                int id = 0;
                int n = 1;
                SqlCommand cm = new SqlCommand("SELECT * FROM Manufacturers", connection);
                SqlDataReader r = cm.ExecuteReader();
                if (r.HasRows)
                {
                    r.Close();
                    cm = new SqlCommand("SELECT MAX(ID_manufacturer) FROM Manufacturers", connection);
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

        private void Insertion()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.InsertManufacturer '" + insertId + "','" + textBox1.Text + "','"+
                id_country[comboBox1.SelectedIndex]+ "'", connect);
                com.ExecuteNonQuery();
            }
            textBox1.Clear();
        }


        private void Manufacturers_Load(object sender, EventArgs e)
        {
            LoadAll();
            Maxid();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                ID_manufacturer = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
                manufacturer = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
                country= dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
                Renaming__street f = new Renaming__street();
                f.Owner = this;
                f.Show();
            }
            else
            {
                MessageBox.Show("Не выбран производитель для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Delete();
            LoadAll();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != ""&&comboBox1.Text!="")
            {
                Insertion();
                Maxid();
                LoadAll();
            }
            else
            {
                MessageBox.Show("Поле пустое.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
