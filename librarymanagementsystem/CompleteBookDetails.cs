using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace librarymanagementsystem
{
    public partial class CompleteBookDetails : Form
    {
        Dbconnection db = new Dbconnection();
        public CompleteBookDetails()
        {
            InitializeComponent();
        }

        private void CompleteBookDetails_Load(object sender, EventArgs e)
        {
            
            string query = "SELECT * FROM issuedbooks WHERE return_date IS NULL";
            db.OpenConnection();
            SQLiteCommand cd = new SQLiteCommand(query, db.myconn);
            SQLiteDataAdapter sda = new SQLiteDataAdapter(cd);
            DataSet ds = new DataSet();

            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            returned();
        }

        public void returned()
        {
            string query = "SELECT * FROM issuedbooks WHERE return_date NOT NULL";
            db.OpenConnection();
            SQLiteCommand cd = new SQLiteCommand(query, db.myconn);
            SQLiteDataAdapter sda = new SQLiteDataAdapter(cd);
            DataSet ds = new DataSet();

            sda.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
        }
    }
}
