using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
//using System.Data.SQLite; 

namespace Covid
{
    //////SQLite Edition 
    class Connection
    {
        string sqlLiteDatabaseName = @"URI=file:C:\Users\Admin\Code\Covid\Covid\CovidDatabase.sqlite3";
        SQLiteConnection conn;

        public Connection()
        {
            try
            {
                this.conn = new SQLiteConnection(this.sqlLiteDatabaseName);
            }
            catch(SQLiteException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.WriteLine("Pripojeny");
            }


        }


        /*
                conn.Open();
        string stm = "SELECT * FROM company LIMIT 5";

        SQLiteCommand cmd = new SQLiteCommand(stm, conn);
        SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine($"{rdr.GetInt32(0)} {rdr.GetString(1)} {rdr.GetInt32(2)}");
            }
        */

        // dbbrowser for sql lite 2
    }

}