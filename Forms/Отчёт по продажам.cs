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
using System.Globalization;


namespace AS_Autodoc
{
    public partial class SalesReport : Form
    {
        string con = Connect.getConnect();
        public SalesReport()
        {
            InitializeComponent();
            SelectComboBox();
        }

        public void SelectComboBox()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Department_store", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        comboBox1.Items.Add(r[0].ToString());
                    }
                }
            }
        }

        static bool TextIsDate(string text)
        {
            var dateFormat = "dd.MM.yyyy";
            DateTime scheduleDate;
            if (DateTime.TryParseExact(text, dateFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out scheduleDate))
            {
                return true;
            }
            return false;
        }

        private void SalesReport_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "Auto_parts_shopDataSetSale.SalesReport". При необходимости она может быть перемещена или удалена.
            this.SalesReportTableAdapter.Fill(this.Auto_parts_shopDataSetSale.SalesReport);
            this.reportViewer1.RefreshReport();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.SalesReportTableAdapter.Fill(this.Auto_parts_shopDataSetSale.SalesReport);
            this.reportViewer1.RefreshReport();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" | comboBox1.Text != "Не указано")
            {
                this.SalesReportTableAdapter.FillByStore(this.Auto_parts_shopDataSetSale.SalesReport, comboBox1.SelectedIndex);
                this.reportViewer1.RefreshReport();
            }
            else
            {
                this.SalesReportTableAdapter.Fill(this.Auto_parts_shopDataSetSale.SalesReport);
                this.reportViewer1.RefreshReport();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text != "  .  ." & maskedTextBox2.Text != "  .  .")
            {
                if (TextIsDate(maskedTextBox1.Text))
                {
                    if (TextIsDate(maskedTextBox2.Text))
                    {
                        this.SalesReportTableAdapter.FillByDate(this.Auto_parts_shopDataSetSale.SalesReport, maskedTextBox1.Text, maskedTextBox2.Text);
                        this.reportViewer1.RefreshReport();
                    }
                    else
                    {
                        MessageBox.Show("Введён неправельный формат даты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        maskedTextBox2.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Введён неправельный формат даты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    maskedTextBox1.Clear();
                }
            }
            else
            {

            }
        }
    }
}
