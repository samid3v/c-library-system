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
    public partial class ViewBooks : Form
    {
        Dbconnection db = new Dbconnection();
        public ViewBooks()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddBook addBk = new AddBook();
            addBk.ShowDialog();
        }

        private void ViewBooks_Load(object sender, EventArgs e)
        {
            loadbooks();
        }
        int bookid;

        public void loadbooks()
        {
            string query = "SELECT * FROM books";
            db.OpenConnection();
            SQLiteCommand cd = new SQLiteCommand(query, db.myconn);
            SQLiteDataAdapter sda = new SQLiteDataAdapter(cd);
            DataSet ds = new DataSet();

            sda.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];

            db.CloseConnection();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                bookid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
            string query = "SELECT * FROM books WHERE id = "+ bookid +" ";
            db.OpenConnection();
            SQLiteCommand cd = new SQLiteCommand(query, db.myconn);
            SQLiteDataAdapter sda = new SQLiteDataAdapter(cd);
            DataSet ds = new DataSet();

            sda.Fill(ds);

            txtBookname2.Text = ds.Tables[0].Rows[0][1].ToString();
            txtAuthorname2.Text = ds.Tables[0].Rows[0][2].ToString();
            txtPublication2.Text = ds.Tables[0].Rows[0][3].ToString();
            dtpPurchasedate2.Text = ds.Tables[0].Rows[0][4].ToString();
            txtBookprice2.Text = ds.Tables[0].Rows[0][5].ToString();
            txtBookQuantity2.Text = ds.Tables[0].Rows[0][6].ToString();

            db.CloseConnection();
        }

        private void txtSearchBookname_TextChanged(object sender, EventArgs e)
        {
            if(txtSearchBookname.Text != "")
            {
                string query = "SELECT * FROM books WHERE book_name LIKE  '" +txtSearchBookname.Text+"%' ";
                db.OpenConnection();
                SQLiteCommand cd = new SQLiteCommand(query, db.myconn);
                SQLiteDataAdapter sda = new SQLiteDataAdapter(cd);
                DataSet ds = new DataSet();

                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                db.CloseConnection();
            }
            else
            {
                string query = "SELECT * FROM books";
                db.OpenConnection();
                SQLiteCommand cd = new SQLiteCommand(query, db.myconn);
                SQLiteDataAdapter sda = new SQLiteDataAdapter(cd);
                DataSet ds = new DataSet();

                sda.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];

                db.CloseConnection();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearchBookname.Clear();
            loadbooks();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string bname =  txtBookname2.Text;
            string bauthor = txtAuthorname2.Text;
            string bpublication = txtPublication2.Text;
            string bpurchasedate = dtpPurchasedate2.Text;
            float bprice = float.Parse(txtBookprice2.Text);
            Int64 bquantity = Int64.Parse(txtBookQuantity2.Text);

            string query = "UPDATE books SET book_name='" + bname + "', author_name= '" + bauthor + "', publication= '" + bpublication + "', publication_date= '" + bpurchasedate + "', book_price=" + bprice + ", book_qty= " + bquantity + " WHERE id = " + bookid + " ";
            db.OpenConnection();
            SQLiteCommand cd = new SQLiteCommand(query, db.myconn);
            cd.ExecuteNonQuery();
            db.CloseConnection();

            MessageBox.Show("successfully Updated", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadbooks();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM books WHERE id = " + bookid + " ";
            db.OpenConnection();
            SQLiteCommand cd = new SQLiteCommand(query, db.myconn);
            cd.ExecuteNonQuery();
            db.CloseConnection();

            MessageBox.Show("successfully deleted", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadbooks();

            clearValues();
            
        }

        public void clearValues()
        {
            txtBookname2.Clear();
            txtAuthorname2.Clear();
            txtPublication2.Clear();

            txtBookprice2.Clear();
            txtBookQuantity2.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearValues();
        }
    }
}
