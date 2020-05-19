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
    public partial class Suppliers : Form
    {
        string con = Connect.getConnect();
        public Suppliers()
        {
            InitializeComponent();

            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Country", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        comboBox1.Items.Add(r[1].ToString());
                    }
                }
            }
        }
        public string InsertOrEdit { get; set; }
        public int ID_supplier { get; set; }
        public string Title { get; set; }
        public decimal TIN { get; set; }
        public decimal CIO { get; set; }
        public string FIO_director { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

        public void LoadAll()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.SelectSuppliers", connect);
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
                        dataGridView1[9, i].Value = r[9].ToString();
                        dataGridView1[10, i].Value = r[10].ToString();
                        i++;
                    }
                }
            }
        }

        private void Delete()
        {
            DialogResult result = MessageBox.Show(
            "Вы точно хотите удалить поставщика?",
            "Предупреждение",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button3);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection connect = new SqlConnection(con))
                {
                    connect.Open();                
                    int id = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
                    SqlCommand com = new SqlCommand("DELETE FROM Supply WHERE ID_supplier = '" + id + "'", connect);
                    com.ExecuteNonQuery();
                    com = new SqlCommand("DELETE FROM Suppliers WHERE ID_supplier = '" + id + "'", connect);
                    com.ExecuteNonQuery();
                }
            }
            this.TopMost = true;
        }

        public void SearchBySupplier()
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

        private void SearchBySupplierAndCountry()
        {
            if (textBox1.Text != "" && comboBox1.Text != "" && comboBox1.Text != "Не указано")
            {
                using (SqlConnection connect = new SqlConnection(con))
                {
                    connect.Open();
                    SqlCommand com = new SqlCommand("EXECUTE dbo.SearchBySupplierAndCountry '"+textBox1.Text+
                    "','"+comboBox1.Text+"'", connect);
                    int i = 0;
                    dataGridView1.Rows.Clear();
                    using (SqlDataReader r = com.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1[0, i].Value = r[0].ToString().TrimEnd();
                            dataGridView1[1, i].Value = r[1].ToString().TrimEnd();
                            dataGridView1[2, i].Value = r[2].ToString().TrimEnd();
                            dataGridView1[3, i].Value = r[3].ToString().TrimEnd();
                            dataGridView1[4, i].Value = r[4].ToString().TrimEnd();
                            dataGridView1[5, i].Value = r[5].ToString().TrimEnd();
                            dataGridView1[6, i].Value = r[6].ToString().TrimEnd();
                            dataGridView1[7, i].Value = r[7].ToString().TrimEnd();
                            dataGridView1[8, i].Value = r[8].ToString().TrimEnd();
                            dataGridView1[9, i].Value = r[9].ToString().TrimEnd();
                            dataGridView1[10, i].Value = r[10].ToString().TrimEnd();
                            i++;
                        }
                    }
                }
            }
        }

        private void Suppliers_Load(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            InsertOrEdit = "Добавить";
            AddingSupplier f = new AddingSupplier();
            f.Owner = this;
            f.Show();
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
                ID_supplier = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
                Title = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
                TIN = Convert.ToDecimal(dataGridView1[2, dataGridView1.CurrentRow.Index].Value);
                CIO = Convert.ToDecimal(dataGridView1[3, dataGridView1.CurrentRow.Index].Value);
                FIO_director = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
                Country = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();
                City = dataGridView1[6, dataGridView1.CurrentRow.Index].Value.ToString();
                Street = dataGridView1[7, dataGridView1.CurrentRow.Index].Value.ToString();
                House= dataGridView1[8, dataGridView1.CurrentRow.Index].Value.ToString();
                Telephone= dataGridView1[9, dataGridView1.CurrentRow.Index].Value.ToString();
                Email = dataGridView1[10, dataGridView1.CurrentRow.Index].Value.ToString();
                InsertOrEdit = "Редактировать";
                AddingSupplier f = new AddingSupplier();
                f.Owner = this;
                f.Show();
            }
            else
            {
                MessageBox.Show("Не выбрана страна для переименования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && (comboBox1.Text == "" || comboBox1.Text == "Не указано"))
            {
                SearchBySupplier();
            }
            else if (textBox1.Text == "" && (comboBox1.Text != "" || comboBox1.Text != "Не указано"))
            {
                SearchByCountry();
            }
            else if(textBox1.Text != "" && (comboBox1.Text != "" || comboBox1.Text != "Не указано"))
            {
                SearchBySupplierAndCountry();
            }
            else
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Visible = true;
                }
            }
        }

        public void SearchByCountry()
        {
            if (comboBox1.Text != "" && comboBox1.Text != "Не указано")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string str = dataGridView1[5, i].Value.ToString();
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

        private void Button5_Click(object sender, EventArgs e)
        {
            SearchByCountry();
        }
    }
}
