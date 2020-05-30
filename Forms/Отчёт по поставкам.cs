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
    public partial class DeliveryReport : Form
    {
        string con = Connect.getConnect();
        public DeliveryReport()
        {
            InitializeComponent();

            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(this.Width, this.Height);
            this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);
            this.StartPosition = FormStartPosition.CenterScreen;

            SelectComboBox();
        }

        void SelectComboBox()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Suppliers", connect);
                using (SqlDataReader r = com.ExecuteReader())
                {
                    while (r.Read())
                    {
                        comboBox1.Items.Add(r[1].ToString());
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

        private void DeliveryReport_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "Auto_parts_shopDataSet.DeliveryReport". При необходимости она может быть перемещена или удалена.
            this.DeliveryReportTableAdapter.Fill(this.Auto_parts_shopDataSet.DeliveryReport);

            this.reportViewer1.RefreshReport();
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FilteringBySuppliers()
        {
            if (comboBox1.Text != "" | comboBox1.Text != "Не указано")
            {
                this.DeliveryReportTableAdapter.FillBySuppliers(this.Auto_parts_shopDataSet.DeliveryReport, comboBox1.SelectedItem.ToString());
                this.reportViewer1.RefreshReport();
            }
            else
            {
                this.DeliveryReportTableAdapter.Fill(this.Auto_parts_shopDataSet.DeliveryReport);
                this.reportViewer1.RefreshReport();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && comboBox1.Text != "Не указано" && maskedTextBox1.Text == "  .  ." & maskedTextBox2.Text == "  .  .")
            {
                FilteringBySuppliers();
            }
            else if (comboBox1.Text == "" && comboBox1.Text == "Не указано" && maskedTextBox1.Text != "  .  ." & maskedTextBox2.Text != "  .  .")
            {
                FilteringByDate();
            }
            else if (comboBox1.Text != "" && comboBox1.Text != "Не указано" && maskedTextBox1.Text != "  .  ." & maskedTextBox2.Text != "  .  .")
            {
                FilteringBySupplierAndDate();
            }
        }

        private void FilteringBySupplierAndDate()
        {
            if (comboBox1.Text != "" && comboBox1.Text != "Не указано" && maskedTextBox1.Text != "  .  ." & maskedTextBox2.Text != "  .  .")
            {
                if (TextIsDate(maskedTextBox1.Text))
                {
                    if (TextIsDate(maskedTextBox2.Text))
                    {
                        this.DeliveryReportTableAdapter.FillBySuppliersAndDate(this.Auto_parts_shopDataSet.DeliveryReport, comboBox1.Text, maskedTextBox1.Text, maskedTextBox2.Text);
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
        }

        private void ReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void FilteringByDate()
        {
            if (maskedTextBox1.Text != "  .  ." & maskedTextBox2.Text != "  .  .")
            {
                if (TextIsDate(maskedTextBox1.Text))
                {
                    if (TextIsDate(maskedTextBox2.Text))
                    {
                        this.DeliveryReportTableAdapter.FillByDate(this.Auto_parts_shopDataSet.DeliveryReport, maskedTextBox1.Text.ToString(), maskedTextBox2.Text.ToString());
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
                this.DeliveryReportTableAdapter.Fill(this.Auto_parts_shopDataSet.DeliveryReport);
                this.reportViewer1.RefreshReport();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click_1(object sender, EventArgs e)
        {
            this.DeliveryReportTableAdapter.Fill(this.Auto_parts_shopDataSet.DeliveryReport);
            this.reportViewer1.RefreshReport();
        }
    }
}
