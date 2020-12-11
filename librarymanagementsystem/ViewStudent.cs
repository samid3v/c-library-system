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

    public partial class ViewStudent : Form
    {
        Dbconnection db = new Dbconnection();
        public ViewStudent()
        {
            InitializeComponent();
        }

        private void ViewStudent_Load(object sender, EventArgs e)
        {
            loadStudents();
        }

        public void loadStudents()
        {
            string query = "SELECT * FROM studinfo";
            db.OpenConnection();
            SQLiteCommand cd = new SQLiteCommand(query, db.myconn);
            SQLiteDataAdapter sda = new SQLiteDataAdapter(cd);
            DataSet ds = new DataSet();

            sda.Fill(ds);

            dataGridViewStudentInfo.DataSource = ds.Tables[0];

            db.CloseConnection();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        int studid = 0;
        private void dataGridViewStudentInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewStudentInfo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                studid = int.Parse(dataGridViewStudentInfo.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
            try
            {
                string query = "SELECT * FROM studinfo WHERE id = " + studid + " ";
                db.OpenConnection();
                SQLiteCommand cd = new SQLiteCommand(query, db.myconn);
                SQLiteDataAdapter sda = new SQLiteDataAdapter(cd);
                DataSet ds = new DataSet();

                sda.Fill(ds);

                txtStudentName.Text = ds.Tables[0].Rows[0][1].ToString();
                txtAdmNo.Text = ds.Tables[0].Rows[0][2].ToString();
                txtDepartment.Text = ds.Tables[0].Rows[0][3].ToString();
                txtSemester.Text = ds.Tables[0].Rows[0][4].ToString();
                txtContact.Text = ds.Tables[0].Rows[0][5].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0][6].ToString();

                db.CloseConnection();
            }
            catch (ArgumentOutOfRangeException arex)
            {
                MessageBox.Show(arex.Message);
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

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            string sname = txtStudentName.Text;
            Int64 sAdmno =Int64.Parse(txtAdmNo.Text);
            string department = txtDepartment.Text;
            string semester = txtSemester.Text;
            string contact = txtContact.Text;
            string email = txtEmail.Text;

            string query = "UPDATE studinfo SET sname='" + sname + "', department= '" + department + "', semester= '" + semester + "', contact='" + contact + "', email= '" + email + "' WHERE admno = '" + sAdmno + "' ";
            db.OpenConnection();
            SQLiteCommand cd = new SQLiteCommand(query, db.myconn);
            cd.ExecuteNonQuery();
            db.CloseConnection();

            MessageBox.Show("successfully Updated", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadStudents();
            clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Int64 sAdmno = Int64.Parse(txtAdmNo.Text);

            string query = "DELETE FROM studinfo WHERE admno = '" + sAdmno + "' ";
            db.OpenConnection();
            SQLiteCommand cd = new SQLiteCommand(query, db.myconn);
            cd.ExecuteNonQuery();
            db.CloseConnection();
            MessageBox.Show("successfully Deleted", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadStudents();
            clear();
        }
    }
}
