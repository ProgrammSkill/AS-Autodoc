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
    public partial class Distribution : Form
    {
        string con = Connect.getConnect();
        List<int> id_department;
        public int insertIdAvailability;
        public Distribution()
        {
            InitializeComponent();

            id_department = new List<int>();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Department_store", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        id_department.Add(Convert.ToInt32(r[0]));
                        comboBox1.Items.Add(r[0].ToString());
                    }
                }
            }
        }

        public string ID_availability;
        public string ID_department;
        public string ID_autoparts;
        public string Price_holiday;
        public string Amount;

        void MaxIdAvailability()
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                int id = 1;
                SqlCommand cm = new SqlCommand("SELECT * FROM Availability_auto_parts", connection);
                SqlDataReader r = cm.ExecuteReader();
                if (r.HasRows)
                {
                    r.Close();
                    cm = new SqlCommand("SELECT MAX(ID_availability) FROM Availability_auto_parts", connection);
                    r = cm.ExecuteReader();
                    while (r.Read())
                    {
                        id = Convert.ToInt32(r[0]) + 1;
                        insertIdAvailability = id;
                    }
                }
                else insertIdAvailability = 1;
            }
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void ToDistribute()
        {
            Supply f = (Supply)this.Owner;
            int department = id_department[comboBox1.SelectedIndex];
            int autopart = f.id_autopart;
            decimal price = Convert.ToDecimal(f.price_holiday.ToString().Replace(".", ","));
            int amount = f.amount;

            using (SqlConnection connect = new SqlConnection(con))
            {
                SqlDataAdapter sda = new SqlDataAdapter("dbo.CheckAutoparts " + department + "," + autopart, connect);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() != "1")
                {
                    connect.Open();
                    SqlCommand com = new SqlCommand("EXECUTE dbo.InsertAvailability_auto_parts " + insertIdAvailability +
                    "," + department + "," + autopart + "," + Convert.ToString(price).Replace(",", ".") + "," + amount, connect);
                    com.ExecuteNonQuery();
                }
                else
                {
                    connect.Open();
                    SqlCommand com = new SqlCommand("SELECT * FROM Availability_auto_parts WHERE ID_department=" + department +
                    " AND ID_autoparts=" + autopart + "", connect);
                    using (SqlDataReader r = com.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            ID_availability = r[0].ToString();
                            ID_department = r[1].ToString();
                            ID_autoparts = r[2].ToString();
                            Price_holiday = r[3].ToString();
                            Amount = r[4].ToString();
                        }
                    }

                    decimal p = Convert.ToDecimal(Price_holiday.ToString().Replace(".", ","));
                    int a = Convert.ToInt32(Amount);
                    decimal max;

                    if (price > p)
                    {
                        max = price;
                    }
                    else
                    {
                        max = p;
                    }
                    string MaxStr = Convert.ToString(max).Replace(",", ".");
                    int newAmount = a + Convert.ToInt32(amount);

                    connect.Close();
                    connect.Open();
                    SqlCommand EditAvailability = new SqlCommand("EXECUTE dbo.EditAvailability_auto_parts " + Convert.ToInt32(ID_availability) +
                    "," + Convert.ToInt32(ID_department) + "," + Convert.ToInt32(ID_autoparts) + "," + MaxStr + "," + newAmount, connect);
                    EditAvailability.ExecuteNonQuery();
                }
            }

            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("EXECUTE dbo.StatusChange " + f.id_supply+
                ",'Распределена'", connect);
                com.ExecuteNonQuery();
            }
        }

        private void Distribution_Load(object sender, EventArgs e)
        {
            MaxIdAvailability();
            Supply f = (Supply)this.Owner;
            label1.Text += " " + f.supplier.ToString();
            label2.Text += " " + f.autopart.ToString();
            label3.Text += " " + f.manufacturer.ToString();
            label4.Text += " " + f.article.ToString();
            label5.Text += " " + f.price_holiday.ToString();
            label6.Text += " " + f.amount.ToString();
            label7.Text += " " + f.sum.ToString();
            label8.Text += " " + f.delivery_date.ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ToDistribute();
            Supply f = (Supply)this.Owner;
            f.LoadAll();
            MaxIdAvailability();
            MessageBox.Show("Поставка распределена");
            this.Close();
        }
    }
}
