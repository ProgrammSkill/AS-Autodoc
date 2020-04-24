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
    public partial class StorekeeperMenu : Form
    {
        string con = Connect.getConnect();
        public StorekeeperMenu()
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
                SqlCommand cm = new SqlCommand("SELECT * FROM UserSession", connection);
                SqlDataReader r = cm.ExecuteReader();
                if (r.HasRows)
                {
                    r.Close();
                    cm = new SqlCommand("SELECT MAX(ID_session) FROM UserSession", connection);
                    r = cm.ExecuteReader();
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
                SqlCommand com = new SqlCommand("EXECUTE dbo.InsertUserSession '" + insertId + "','" + f.Login.ToString() + "','" + DateTime.Now + "'", connect);
                com.ExecuteNonQuery();
            }
        }

        private void StorekeeperMenu_Load(object sender, EventArgs e)
        {
            Authorization_form f = (Authorization_form)this.Owner;
            label1.Text += " " + f.Surname.ToString().TrimEnd() + " " + f.First_name.ToString().TrimEnd() + " " + f.Last_name.ToString().TrimEnd();
            label2.Text += " " + f.Role.ToString().TrimEnd();
            MaxId();
            InsertUserSession();
        }

        private void ОПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutTheProgram f = new AboutTheProgram();
            f.ShowDialog();
        }

        private void СменитьПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ВыходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Authorization_form f = (Authorization_form)this.Owner;
            f.Close();
        }

        private void АвтозапчастиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Autoparts f = new Autoparts();
            f.ShowDialog();
        }

        private void АвтозапчастиВМагазинеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoPartsInStore f = new AutoPartsInStore();
            f.ShowDialog();
        }
    }
}
