using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace librarymanagementsystem
{
    
    class Dbconnection
    {
        public SQLiteConnection myconn;

        public Dbconnection()
        {
            myconn = new SQLiteConnection("Data Source = librarysystem.sqlite3");
            if (!File.Exists("./librarysystem.sqlite3"))
            {
                SQLiteConnection.CreateFile("librarysystem.sqlite3");
            }
        }
        public void OpenConnection()
        {
            if (myconn.State != System.Data.ConnectionState.Open)
            {
                myconn.Open();
            }
        }
        public void CloseConnection()
        {
            if (myconn.State != System.Data.ConnectionState.Closed)
            {
                myconn.Close();
            }
        }

    }
}
