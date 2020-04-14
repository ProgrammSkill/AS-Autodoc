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
        public string InsertOrEdit { get; set; }

        public int id_supply;
        public string supplier;
        public string autopart;
        public string price_holiday;
        public int amount;
        public string delivery_date;

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
            InsertOrEdit = "Добавить";
            AddingAndEditingDelivery f = new AddingAndEditingDelivery();
            f.Owner = this;
            f.ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                InsertOrEdit = "Редактировать";
                id_supply = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
                supplier = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
                autopart = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
                price_holiday = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
                amount = Convert.ToInt32(dataGridView1[4, dataGridView1.CurrentRow.Index].Value);
                delivery_date = dataGridView1[6, dataGridView1.CurrentRow.Index].Value.ToString();

                AddingAndEditingDelivery f = new AddingAndEditingDelivery();
                f.Owner = this;
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана поставка для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
