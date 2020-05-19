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
    public partial class DepartmentStore : Form
    {
        string con = Connect.getConnect();
        int insertId;
        List<int> id_city;
        List<int> id_street;
        public DepartmentStore()
        {
            InitializeComponent();

            id_city = new List<int>();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM City", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_city.Add(Convert.ToInt32(r[0]));
                        comboBox1.Items.Add(r[1].ToString());
                    }
                }
            }

            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT DISTINCT City FROM City INNER JOIN Department_store" +
                " ON Department_store.ID_city=City.ID_city", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        comboBox3.Items.Add(r[0].ToString());
                    }
                }
            }

            id_street = new List<int>();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Street", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_street.Add(Convert.ToInt32(r[0]));
                        comboBox2.Items.Add(r[1].ToString());

                    }

                }

            }
        }
        public int id_department;
        public string city;
        public string street;
        public string house;
        public string telephone;

        public void LoadAll()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.SelectDepartment_store", connect);
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
                        i++;
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
                SqlCommand cm = new SqlCommand("SELECT * FROM Department_store", connection);
                SqlDataReader r = cm.ExecuteReader();
                if (r.HasRows)
                {
                    r.Close();
                    cm = new SqlCommand("SELECT MAX(ID_department) FROM Department_store", connection);
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
                SqlCommand com = new SqlCommand("EXECUTE dbo.InsertDepartment_store '" + insertId + "','" 
                + id_city[comboBox1.SelectedIndex]+"','" + id_street[comboBox2.SelectedIndex] +
                "','" + textBox1.Text + "','"+maskedTextBox1.Text+"'", connect);
                com.ExecuteNonQuery();
            }
            comboBox1.Text="";
            comboBox2.Text="";
            textBox1.Clear();
            maskedTextBox1.Text = "";
        }

        private void DepartmentStore_Load(object sender, EventArgs e)
        {
            LoadAll();
            MaxId();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && comboBox2.Text != "" && textBox1.Text != "" && maskedTextBox1.Text != "")
            {
                Insertion();
                MaxId();
                LoadAll();
            }
            else
            {
                MessageBox.Show("Заполните все поля!.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                id_department= Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
                city = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
                street = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
                house= dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
                telephone = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();

                EditingDepartment f = new EditingDepartment();
                f.Owner = this;
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбран отдел для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text != "" && comboBox3.Text != "Не указано")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string str = dataGridView1[1, i].Value.ToString();
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

        private void Delete()
        {
            string title_country = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            DialogResult result = MessageBox.Show(
            "Вы точно хотите удалить отдел магазина?",
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
                    SqlCommand com = new SqlCommand("DELETE FROM Availability_auto_parts WHERE ID_department='" + id + "'", connect);
                    com.ExecuteNonQuery();
                    com = new SqlCommand("DELETE FROM Department_store WHERE ID_department='" + id + "'", connect);
                    com.ExecuteNonQuery();

                }
            }
            this.TopMost = true;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Delete();
            LoadAll();
        }
    }
}
