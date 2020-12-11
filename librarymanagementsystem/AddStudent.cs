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
    public partial class AddStudent : Form
    {
        Dbconnection db = new Dbconnection();
        public AddStudent()
        {
            InitializeComponent();
        }

        private void label12_Click(object sender, EventArgs e)
        {

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

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            try
            {
                string studName = txtStudentName.Text;
                Int64 admNo = Int64.Parse(txtAdmNo.Text);
                string department = txtDepartment.Text;
                string semester = txtSemester.Text;
                Int64 contact = Int64.Parse(txtContact.Text);
                string email = txtEmail.Text;

                string query = "INSERT INTO studinfo(sname, admno, department, semester, contact, email) VALUES('" + studName + "', " + admNo + ", '" + department + "', '" + semester + "', " + contact + ", '" + email + "' )";
                db.OpenConnection();
                SQLiteCommand cd = new SQLiteCommand(query, db.myconn);
                cd.ExecuteNonQuery();

                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
