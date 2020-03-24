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
        }

        public void LoadAll()
        {
            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.SelectSuppliers", conn);
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
                    SqlCommand com = new SqlCommand("EXECUTE dbo.DeleteSupplier '" + id + "'", connect);
                    com.ExecuteNonQuery();

                }
            }
            this.TopMost = true;
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
            AddingSupplier f = new AddingSupplier();
            f.Owner = this;
            f.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Delete();
            LoadAll();
        }
    }
}
