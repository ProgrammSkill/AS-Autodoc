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
    public partial class AutoPartsInStore : Form
    {
        string con = Connect.getConnect();
        public AutoPartsInStore()
        {
            InitializeComponent();
        }
        public string InsertOrEdit;
        public int ID_availability;
        public int ID_department;
        public string Title_auto_part;
        public string Manufacturer;
        public string Article;
        public string Sale_price;
        public int Amount;

        public void LoadAll()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.SelectAvailability_auto_parts", connect);
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
                        i++;
                    }
                }
            }
        }

        private void AutoPartsInStore_Load(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            InsertOrEdit = "Добавить";
            AddingAndEditingAvailabilityOfAutoParts f = new AddingAndEditingAvailabilityOfAutoParts();
            f.Owner = this;
            f.ShowDialog();
            f.ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                InsertOrEdit = "Редактировать";
                ID_availability = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
                ID_department = Convert.ToInt32(dataGridView1[1, dataGridView1.CurrentRow.Index].Value);
                Title_auto_part = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
                Manufacturer = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
                Article = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
                Sale_price = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();
                Amount = Convert.ToInt32(dataGridView1[6, dataGridView1.CurrentRow.Index].Value);
                AddingAndEditingAvailabilityOfAutoParts f = new AddingAndEditingAvailabilityOfAutoParts();
                f.Owner = this;
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана автозапчасть в магазине для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
