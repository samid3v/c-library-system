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
    public partial class AddBook : Form
    {
        Dbconnection db = new Dbconnection();
        public AddBook()
        {
            InitializeComponent();
        }

        private void btnAddbook_Click(object sender, EventArgs e)
        {
            string bname = txtBookname.Text;
            string bauthor = txtAuthorname.Text;
            string publication = txtPublication.Text;
            string p_date = dtpPurchasedate.Text;
            float bPrice = float.Parse(txtBookprice.Text);
            Int64 bQuantity = Int64.Parse(txtBookQuantity.Text);

            if (bname != "" && bauthor != "" && publication != "" && bPrice != 0 && bQuantity != 0)
            {

                string query = "INSERT INTO books(book_name, author_name, publication, publication_date, book_price, book_qty) VALUES('" + bname + "', '" + bauthor + "', '" + publication + "', '" + p_date + "', " + bPrice + ", " + bQuantity + " )";
                db.OpenConnection();
                SQLiteCommand cd = new SQLiteCommand(query, db.myconn);
                cd.ExecuteNonQuery();

                //ViewBooks vb = new ViewBooks();
                //vb.loadbooks();

                db.CloseConnection();
                MessageBox.Show("successfully Added", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBookname.Clear();
                txtAuthorname.Clear();
                txtPublication.Clear();
                txtBookprice.Clear();
                txtBookQuantity.Clear();

                
                
            }
            else
            {
                MessageBox.Show("error: All Fields are required", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string bname = txtBookname.Text;
            string bauthor = txtAuthorname.Text;
            string publication = txtPublication.Text;
            string p_date = dtpPurchasedate.Text;
            float bPrice = float.Parse(txtBookprice.Text);
            Int64 bQuantity = Int64.Parse(txtBookQuantity.Text);

            if (bname != "" || bauthor != "" || publication != "" || bPrice != 0 || bQuantity != 0)
            {
                if(MessageBox.Show("Unsaved Data will be lost", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    this.Close();
                }
                
            }
            else
            {
                this.Close();
            }
        }
    }
}
