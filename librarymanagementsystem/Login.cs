using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace librarymanagementsystem
{
    public partial class Login : Form
    {
        Dbconnection db = new Dbconnection();
        public Login()
        {
            InitializeComponent();
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != "" || txtPassword.Text != "")
            {
                string query = "SELECT * FROM users WHERE username='" + txtUsername.Text + "' AND password = '" + txtPassword.Text + "'";
                db.OpenConnection();
                SQLiteCommand cd = new SQLiteCommand(query, db.myconn);
                SQLiteDataAdapter da = new SQLiteDataAdapter(cd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    MessageBox.Show("success", "suuss");
                }
                else
                {
                    MessageBox.Show("error: Password or username is incorrect", "err2");
                }
            }
            else 
            {
                MessageBox.Show("error: All Fields are required", "err1");
            }

        }
    }
}
