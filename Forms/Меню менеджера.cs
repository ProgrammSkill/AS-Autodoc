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
    public partial class ManagerMenu : Form
    {
        string con = Connect.getConnect();
        public ManagerMenu()
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
                SqlCommand com = new SqlCommand("EXECUTE dbo.InsertUserSession '" + insertId + "','" + f.Login.ToString()+ "','" +DateTime.Now+"'", connect);
                com.ExecuteNonQuery();
            }
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            Authorization_form f = (Authorization_form)this.Owner;
            label1.Text += " "+f.Surname.ToString().TrimEnd()+" " + f.First_name.ToString().TrimEnd()+" "+f.Last_name.ToString().TrimEnd();
            label2.Text += " "+f.Role.ToString().TrimEnd();
            MaxId();
            InsertUserSession();
        }

        private void Enter_Click(object sender, EventArgs e)
        {
            Suppliers f = new Suppliers();
            f.ShowDialog();
        }

        private void СтранаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Country f = new Country();
            f.ShowDialog();
        }

        private void ГородToolStripMenuItem_Click(object sender, EventArgs e)
        {
            City f = new City();
            f.ShowDialog();
        }

        private void УлицаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Street f = new Street();
            f.ShowDialog();
        }

        private void МоделиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Models f = new Models();
            f.ShowDialog();
        }

        private void МаркиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Brands f = new Brands();
            f.ShowDialog();
        }

        private void АвтозапчастиToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ПроизводителиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manufacturers f = new Manufacturers();
            f.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Brands_and_models f = new Brands_and_models();
            f.ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Autoparts f = new Autoparts();
            f.ShowDialog();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Supply f = new Supply();
            f.ShowDialog();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Sale f = new Sale();
            f.ShowDialog();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            DepartmentStore f = new DepartmentStore();
            f.ShowDialog();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Sale f = new Sale();
            f.LoadAll();
        }
    }
}
