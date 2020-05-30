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

            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(this.Width, this.Height);
            this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);
            this.StartPosition = FormStartPosition.CenterScreen;

            //Style style = new Style();
            //style.GetSelfAndChildrenRecursive(this).OfType<Button>().ToList()
            //.ForEach(button => button.BackColor = Color.FromArgb(169, 4, 21));

            //style.GetSelfAndChildrenRecursive(this).OfType<Button>().ToList()
            //.ForEach(button => button.ForeColor = Color.White);

            //          style.GetSelfAndChildrenRecursive(this).OfType<Button>().ToList()
            //      .ForEach(button => button.BackColor = Color.FromArgb(169, 4, 21));
        }
        public string Login;
        public string Surname;
        public string First_name;
        public string Last_name;
        public string Role;
        public int i;

        public void Authorization()
        {
            int j = 0;

            using (SqlConnection connect = new SqlConnection(con))
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    connect.Open();
                    SqlCommand com = new SqlCommand("EXECUTE dbo.SelectUserAuthentication '" + textBox1.Text + "'", connect);
                    using (SqlDataReader r = com.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            Login = r[0].ToString();
                            Surname = r[1].ToString();
                            First_name = r[2].ToString();
                            Last_name = r[3].ToString();
                            Role = r[4].ToString();
                        }
                    }
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM Role_ join Users ON Role_.ID_role=Users.ID_role AND Login_='" + textBox1.Text +
                    "'AND Password_= '" + textBox2.Text + "' AND Role_='Менеджер'", connect);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        ManagerMenu f = new ManagerMenu();
                        f.Owner = this;
                        f.FormClosing += F_FormClosing;
                        i = 0;
                        f.Show();
                        Hide();
                    }
                    sda = new SqlDataAdapter("SELECT COUNT(*) FROM Role_ join Users ON Role_.ID_role=Users.ID_role AND Login_='" + textBox1.Text +
                    "'AND Password_= '" + textBox2.Text + "' AND Role_='Кладовшик           '", connect);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        StorekeeperMenu f = new StorekeeperMenu();
                        f.Owner = this;
                        f.FormClosing += F_FormClosing;
                        i = 0;
                        f.Show();
                        Hide();
                    }
                    sda = new SqlDataAdapter("SELECT COUNT(*) FROM Role_ join Users ON Role_.ID_role=Users.ID_role AND Login_='" + textBox1.Text +
                    "'AND Password_= '" + textBox2.Text + "' AND Role_='Директор           '", connect);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        DirectorMenu f = new DirectorMenu();
                        f.Owner = this;
                        f.FormClosing += F_FormClosing;
                        i = 0;
                        f.Show();
                        Hide();
                    }
                    sda = new SqlDataAdapter("SELECT COUNT(*) FROM Role_ join Users ON Role_.ID_role=Users.ID_role AND Login_='" + textBox1.Text +
                    "'AND Password_= '" + textBox2.Text + "' AND Role_='Администратор           '", connect);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        AdminMenu f = new AdminMenu();
                        f.Owner = this;
                        f.FormClosing += F_FormClosing;
                        i = 0;
                        f.Show();
                        Hide();
                    }
                    sda = new SqlDataAdapter("SELECT COUNT(*) FROM Role_ join Users ON Role_.ID_role=Users.ID_role AND Login_='" + textBox1.Text +
                    "'AND Password_= '" + textBox2.Text + "' AND Role_='Продавец'", connect);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        SellerMenu f = new SellerMenu();
                        f.Owner = this;
                        f.FormClosing += F_FormClosing;
                        i = 0;
                        f.Show();
                        Hide();
                    }

                    sda = new SqlDataAdapter("SELECT COUNT(*) FROM Users WHERE Login_= '" + textBox1.Text + "' and Password_= '" + textBox2.Text + "'", connect);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() != "1")
                    {
                        MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        j++;
                        i += j;
                        if (i > 2)
                        {
                            Close();
                        }
                    }
                    textBox1.Clear();
                    textBox2.Clear();
                }
                else
                {
                    MessageBox.Show("Введите логин и пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }
    }
}
