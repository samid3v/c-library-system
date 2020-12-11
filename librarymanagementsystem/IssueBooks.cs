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
    public partial class IssueBooks : Form
    {
        Dbconnection db = new Dbconnection();
        public IssueBooks()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void IssueBooks_Load(object sender, EventArgs e)
        {
            LoadBookName();
           
        }
        public void LoadBookName()
        {
            string query = "SELECT book_name FROM books";
            db.OpenConnection();
            SQLiteCommand cd = new SQLiteCommand(query, db.myconn);
            SQLiteDataReader dr = cd.ExecuteReader();

            while (dr.Read())
            {
                for(int i=0; i<dr.FieldCount; i++)
                {
                    cbxBookName.Items.Add(dr.GetString(i));
                }
            }
        }
        public void clear()
        {
            txtStudentName.Clear();
            txtAdmNo.Clear();
            txtDepartment.Clear();
            txtSemester.Clear();
            txtContact.Clear();
            txtEmail.Clear();
        }
        int count;
        private void btnSearchStudent_Click(object sender, EventArgs e)
        {
            
            if(txtAdmission.Text != "")
            {
                string sAdmno = txtAdmission.Text;
                string query = "SELECT * FROM studinfo WHERE admno = " + sAdmno + " ";
                db.OpenConnection();
                SQLiteCommand cd = new SQLiteCommand(query, db.myconn);
                SQLiteDataAdapter sda = new SQLiteDataAdapter(cd);
                DataSet ds = new DataSet();

                sda.Fill(ds);

                CountBooks();

                if (ds.Tables[0].Rows.Count != 0)
                {
                    txtStudentName.Text = ds.Tables[0].Rows[0][1].ToString();
                    txtAdmNo.Text = ds.Tables[0].Rows[0][2].ToString();
                    txtDepartment.Text = ds.Tables[0].Rows[0][3].ToString();
                    txtSemester.Text = ds.Tables[0].Rows[0][4].ToString();
                    txtContact.Text = ds.Tables[0].Rows[0][5].ToString();
                    txtEmail.Text = ds.Tables[0].Rows[0][6].ToString();

                    db.CloseConnection();
                }
                else
                {
                    MessageBox.Show("Student does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                
            }
            else
            {
                clear();
                MessageBox.Show("Admission is Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        

        public void CountBooks()
        {
            string sAdmno = txtAdmission.Text;
            string query1 = "SELECT count(admno) FROM issuedbooks WHERE admno = '" + sAdmno + "' ";
            db.OpenConnection();
            SQLiteCommand cd1 = new SQLiteCommand(query1, db.myconn);
            SQLiteDataAdapter sda1 = new SQLiteDataAdapter(cd1);
            DataSet ds1 = new DataSet();

            sda1.Fill(ds1);

            count = int.Parse(ds1.Tables[0].Rows[0][0].ToString());
        }

        private void btnIssueBook_Click(object sender, EventArgs e)
        {
            if(count <= 2)
            {
                if(cbxBookName.SelectedIndex != -1)
                {
                    //=============================================================
                    if(txtAdmNo.Text != "")
                    {
                        string bkName = cbxBookName.Text;
                        string query1 = "SELECT book_name FROM issuedbooks WHERE admno = '" + txtAdmission.Text + "' AND book_name='" + bkName + "' ";
                        db.OpenConnection();
                        SQLiteCommand cd1 = new SQLiteCommand(query1, db.myconn);
                        SQLiteDataAdapter sda1 = new SQLiteDataAdapter(cd1);
                        DataTable ds1 = new DataTable();

                        sda1.Fill(ds1);

                        if (ds1.Rows.Count == 1)
                        {
                            MessageBox.Show("Student has not returned this book", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            string query = "INSERT INTO issuedbooks(admno, book_name, issue_date) VALUES(" + txtAdmission.Text + ", '" + cbxBookName.Text + "','" + dtpIssueDate.Text + "')";
                            db.OpenConnection();
                            SQLiteCommand cd = new SQLiteCommand(query, db.myconn);
                            cd.ExecuteNonQuery();
                            MessageBox.Show("Book issued", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clear();
                            txtAdmission.Clear();
                            db.CloseConnection();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter a valid Admission number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    //=============================================================



                   
                }
                else
                {
                    MessageBox.Show("book name is Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("student has three books not returned", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
