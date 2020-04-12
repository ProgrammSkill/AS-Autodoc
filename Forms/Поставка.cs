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
    public partial class Supply : Form
    {
        string con = Connect.getConnect();
        public Supply()
        {
            InitializeComponent();
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
                        i++;
                    }
                }
            }
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
            AddingAndEditingDelivery f = new AddingAndEditingDelivery();
            f.ShowDialog();
        }
    }
}
