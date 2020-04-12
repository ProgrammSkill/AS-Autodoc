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
    public partial class Brands_and_models : Form
    {
        string con = Connect.getConnect();
        List<int> id_brand;
        List<int> id_model;
        public Brands_and_models()
        {
            InitializeComponent();
            id_brand = new List<int>();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Brands", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_brand.Add(Convert.ToInt32(r[0]));
                        comboBox1.Items.Add(r[1].ToString());
                    }
                }
            }

            id_model = new List<int>();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Models", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_model.Add(Convert.ToInt32(r[0]));
                        comboBox2.Items.Add(r[1].ToString());
                    }
                }
            }
        }
        public int insertId;
        public int ID_brd_mdl;
        public string title_brand;
        public string title_model;

        public void LoadAll()
        {
            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("dbo.SelectBrands_and_models", conn);
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
                    SqlCommand com = new SqlCommand("DELETE FROM Brands_and_models WHERE ID_brd_mdl='" + id + "'", connect);
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
                SqlCommand cm = new SqlCommand("SELECT * FROM Brands_and_models", connection);
                SqlDataReader r = cm.ExecuteReader();
                if (r.HasRows)
                {
                    r.Close();
                    cm = new SqlCommand("SELECT MAX(ID_brd_mdl) FROM Brands_and_models", connection);
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
                SqlCommand com = new SqlCommand("EXECUTE dbo.InsertBrand_and_model '" + insertId + "','" +
                id_brand[comboBox1.SelectedIndex] + "','"+id_model[comboBox2.SelectedIndex] +"'", connect);
                com.ExecuteNonQuery();
            }
        }

        private void Brands_and_models_Load(object sender, EventArgs e)
        {
            LoadAll();
            Maxid();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Delete();
            LoadAll();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != ""&& comboBox2.Text != "")
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

        private void Button2_Click(object sender, EventArgs e)
        {

        }
    }
}
