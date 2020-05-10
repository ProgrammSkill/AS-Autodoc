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
    public partial class SellerMenu : Form
    {
        string con = Connect.getConnect();
        public SellerMenu()
        {
            InitializeComponent();
        }
        public int insertId;

        void MaxId()
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                int id = 0;
                int n = 1;
                SqlCommand com = new SqlCommand("SELECT * FROM UserSession", connection);
                SqlDataReader r = com.ExecuteReader();
                if (r.HasRows)
                {
                    r.Close();
                    com = new SqlCommand("SELECT MAX(ID_session) FROM UserSession", connection);
                    r = com.ExecuteReader();
                    while (r.Read())
                    {
                        id = Convert.ToInt32(r[0]) + 1;
                        insertId = id;
                    }
                }
                else insertId = n;
            }
        }

        public void InsertUserSession()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                Authorization_form f = (Authorization_form)this.Owner;
                SqlCommand com = new SqlCommand("EXECUTE dbo.InsertUserSession '" + insertId + "','" + f.Login.ToString() +
                "','" + DateTime.Now.ToString("dd.MM.yyyy") + "','" + DateTime.Now.ToString("hh:mm:ss") + "'", connect);
                com.ExecuteNonQuery();
            }
        }

        private void SellerMenu_Load(object sender, EventArgs e)
        {
            Authorization_form f = (Authorization_form)this.Owner;
            label1.Text += " " + f.Surname.ToString().TrimEnd() + " " + f.First_name.ToString().TrimEnd() + " " + f.Last_name.ToString().TrimEnd();
            label2.Text += " " + f.Role.ToString().TrimEnd();
            MaxId();
            InsertUserSession();
        }

        private void ПродажаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sale f = new Sale();
            f.ShowDialog();
        }

        private void ВыходToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void СменитьПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ОПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutTheProgram f = new AboutTheProgram();
            f.ShowDialog();
        }

        private void АвтозапчастиВМагазинеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoPartsInStoreForSeller f = new AutoPartsInStoreForSeller();
            f.ShowDialog();
        }
    }
}
