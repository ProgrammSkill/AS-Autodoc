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

            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(this.Width, this.Height);
            this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);
            this.StartPosition = FormStartPosition.CenterScreen;

            foreach (ToolStripMenuItem m in menuStrip1.Items)
            {
                SetWhiteColor(m);
            }
            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new Cols());
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

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void SetWhiteColor(ToolStripMenuItem item)
        {
            item.ForeColor = Color.White;
            foreach (ToolStripMenuItem it in item.DropDownItems)
            {
                SetWhiteColor(it);
            }
        }


        public class Cols : ProfessionalColorTable
        {
            Color MyGreen = Color.FromArgb(80, 174, 168);

            public override Color MenuItemSelected
            {
                get { return Color.FromArgb(96, 208, 201); }
            }

            public override Color ToolStripDropDownBackground
            {
                get { return MyGreen; }
            }

            public override Color ImageMarginGradientBegin
            {
                get { return MyGreen; }
            }

            public override Color ImageMarginGradientEnd
            {
                get { return MyGreen; }
            }

            public override Color ImageMarginGradientMiddle
            {
                get { return MyGreen; }
            }

            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.FromArgb(96, 208, 201); }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.FromArgb(96, 208, 201); }
            }

            public override Color MenuItemPressedGradientBegin
            {
                get { return Color.FromArgb(96, 208, 201); }
            }

            public override Color MenuItemPressedGradientMiddle
            {
                get { return Color.FromArgb(96, 208, 201); }
            }

            public override Color MenuItemPressedGradientEnd
            {
                get { return Color.FromArgb(96, 208, 201); }
            }

            public override Color MenuItemBorder
            {
                get { return Color.FromArgb(96, 208, 201); }
            }
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
