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
        // TODO: Overit, ci moze byt taketo adresovanie (povodne bolo =@"URI=file:C:\Users\Admin\Code\Covid\Covid\CovidDatabase.sqlite3")
        string sqlLiteDatabaseName = "URI=file:" + Environment.CurrentDirectory + "\\CovidDatabase.sqlite3";
        public SQLiteConnection conn;

        public Connection()
        {
            try
            {
                this.conn = new SQLiteConnection(this.sqlLiteDatabaseName);
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.WriteLine("Pripojeny");
            }
        }

 
        // TODO: DELETE THIS :D
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

    }

}