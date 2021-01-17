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


        // TODO: DELETE THIS , JUST EXAMPLES HOW WORK WITH DATABASE :D

        /*
        // NACITANIE DAT Z DATABAZY
        void SQLiteReader(Connection db, string table, ref List<List<string>> zaznam)
        {
            db.conn.Open();
            
            if (table == "company")
            {
                string stm = "SELECT * FROM company LIMIT 500"; // TENTO LIMIT TREBA ZVAZIT!
                SQLiteCommand cmd = new SQLiteCommand(stm, db.conn);
                SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    List<string> tmp = new List<string>();
                    tmp.Add(rdr.GetInt32(0).ToString());
                    tmp.Add(rdr.GetString(1));
                    tmp.Add(rdr.GetString(2));
                    zaznam.Add(tmp);
                }
            }

            db.conn.Close();
        }
        */


        /*
        // Vypis dat na CW
        List<List<string>> zaznamSQL = new List<List<string>>();
        SQLiteReader(db, "company", ref zaznamSQL);
        Console.WriteLine("VYPIS DAT ZO SQL:");
        foreach (var i in zaznamSQL)
        {
        foreach (var j in i)
        Console.Write(j);
        Console.WriteLine("");
        }
        */



        /*
        // ZAPIS DAT DO DATABAZY
        if (table == "company")
        {
                // ZAPIS ZAZNAMU S 2 TEXTAMI DO DATABAZY ORGANIZACII
                foreach (var i in zaznam)
                {
                    try
                    {
                        cmd.CommandText = "INSERT INTO company(Id, Name, Address) VALUES(@id, @name, @address)";
                        cmd.Parameters.AddWithValue("@id", ++posledneID);
                        cmd.Parameters.AddWithValue("@name", i[0]);
                        cmd.Parameters.AddWithValue("@address", i[1]);
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Neočakávaná chyba pri zápise do databázy (organizácia - {errorId})!", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine(ex.ToString());
                        return;
                    }
                }
        }
        */


        /*
                // ZISKANIE ID POSLEDNEHO ZAZNAMU ORGANIZACIE, PRE POTREBY PRIDANIA NOVEHO S INYM ID
                string stm = "SELECT * FROM company LIMIT 1000"; // TODO: prediskutovat limit zaznamov (1000)
                SQLiteCommand tmpCmd = new SQLiteCommand(stm, db.conn);
                SQLiteDataReader rdr = tmpCmd.ExecuteReader();
                while (rdr.Read())
                    posledneID = rdr.GetInt32(0);
        */
    }
}