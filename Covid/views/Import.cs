using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Covid
{
    public partial class Import : Form
    {
        public Import()
        {
            InitializeComponent();
        }

        private void g2b_import_Click(object sender, EventArgs e)
        {
            Connection db = new Connection();
            
            List<List<string>> zaznamCSV =new List<List<string>>();
            List<List<string>> zaznamSQL = new List<List<string>>();


            CSVReader("\\data\\skoly.csv", ref zaznamCSV);

            Console.WriteLine("VYPIS DAT Z CSV:");
            foreach (var i in zaznamCSV)
            {
                foreach (var j in i)
                    Console.Write(j);
                Console.WriteLine("");
            }

            SQLiteWriter(db, "company", zaznamCSV);

            SQLiteReader(db, "company", ref zaznamSQL);

            Console.WriteLine("VYPIS DAT ZO SQL:");
            foreach (var i in zaznamSQL)
            {
                foreach (var j in i)
                    Console.Write(j);
                Console.WriteLine("");
            }


        }

        void CSVReader(string filePath, ref List<List<string>> zaznam)
        {
            try
            {
                using (var reader = new StreamReader(Environment.CurrentDirectory + filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string riadok = reader.ReadLine();
                        var dataZRiadku = riadok.Split(';');

                        List<string> tmp = new List<string>();
                        foreach (string i in dataZRiadku)
                        {
                            tmp.Add(i);
                        }
                        zaznam.Add(tmp);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR", "Chyba pri čítaní zo súboru CSV!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.ToString());
            }
        }

        void SQLiteWriter(Connection db, string table, List<List<string>> zaznam)
        {
            db.conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(db.conn);


            if (table == "company")
            {
                foreach (var i in zaznam)
                {
                    cmd.CommandText = "INSERT INTO company(Id, Name, Address) VALUES(@id, @name, @address)";
                    cmd.Parameters.AddWithValue("@id", i[0]);
                    cmd.Parameters.AddWithValue("@name", i[1]);
                    cmd.Parameters.AddWithValue("@address", i[2]);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                    // POZOR NIE JE OSETRENIE PRIDAVANIE ZAZNAMU S ROVNAKYM ID!!!!
                }
            }

            db.conn.Close();
        }

        void SQLiteReader(Connection db, string table, ref List<List<string>> zaznam)
        {
            db.conn.Open();
            
            if (table == "company")
            {
                string stm = "SELECT * FROM company LIMIT 5";
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
    }
}
