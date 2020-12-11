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
    public partial class ReturnBook : Form
    {
        Dbconnection db = new Dbconnection();
        public ReturnBook()
        {
            InitializeComponent();
        }

        public void updatebooks()
        {
            string sAdmno = txtAdmission.Text;
            string query = "SELECT * FROM issuedbooks WHERE admno = '" + sAdmno + "' AND return_date IS NULL";
            db.OpenConnection();
            SQLiteCommand cd = new SQLiteCommand(query, db.myconn);
            SQLiteDataAdapter sda = new SQLiteDataAdapter(cd);
            DataSet ds = new DataSet();

            sda.Fill(ds);
        }
        public void bookload()
        {
            if (txtAdmission.Text != "")
            {
                string sAdmno = txtAdmission.Text;
                string query = "SELECT * FROM issuedbooks WHERE admno = '" + sAdmno + "' AND return_date IS NULL";
                db.OpenConnection();
                SQLiteCommand cd = new SQLiteCommand(query, db.myconn);
                SQLiteDataAdapter sda = new SQLiteDataAdapter(cd);
                DataSet ds = new DataSet();

                sda.Fill(ds);



                if (ds.Tables[0].Rows.Count != 0)
                {

                    dataGridView1.DataSource = ds.Tables[0];
                    db.CloseConnection();
                }
                else
                {
                    MessageBox.Show("invalid amission or no book issued", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearchStudent_Click(object sender, EventArgs e)
        {
           if(txtAdmission.Text != "")
            {
                bookload();
            }
            else
            {
                MessageBox.Show("Enter admission", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void clear()
        {
            txtDateIssued.Clear();
            txtAdmission.Clear();
            txtBookName.Clear();
        }
        string bookname;
        int admno;
        string issue_date;

       

        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                admno = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                bookname = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                issue_date = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
            else
            {
                MessageBox.Show("null cell click", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            txtBookName.Text = bookname;
            txtDateIssued.Text = issue_date;
            
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            string query = "UPDATE issuedbooks SET return_date='" + dateTimePicker1.Text + "' WHERE admno = '" + admno + "' AND book_name='"+ bookname +"' ";
            db.OpenConnection();
            SQLiteCommand cd = new SQLiteCommand(query, db.myconn);
            cd.ExecuteNonQuery();
            db.CloseConnection();
            MessageBox.Show("book returned", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            updatebooks();
            clear();
        }
    }
}
