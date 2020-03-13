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
    public partial class Authorization_form : Form
    {
        string con = Connect.getConnect();
        public Authorization_form()
        {
            InitializeComponent();
        }
        public string login;
        public int i;

        public void Authorization()
        {
            int j = 0;

            using (SqlConnection connect = new SqlConnection(con))
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    connect.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM Users WHERE Login_='" + textBox1.Text +
                    "' and Password_= '" + textBox2.Text + "'", connect);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        //login = textBox1.Text.ToString();
                        Menu f = new Menu();
                        f.Owner = this;
                        f.FormClosing += F_FormClosing;
                        f.Show();
                        Hide();

                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль");
                        j++;
                        i += j;
                        if (i > 3)
                        {
                            Close();
                        }

                    }

                    textBox1.Clear();
                    textBox2.Clear();

                }
                else
                {
                    MessageBox.Show("Заполните все поля");
                }

            }
        }

        private void F_FormClosing(object sender, FormClosingEventArgs e)
        {
            Show();
        }

        private void Authorization_Load(object sender, EventArgs e)
        {

        }

        private void Enter_Click(object sender, EventArgs e)
        {
            Authorization();
        }
    }
}
