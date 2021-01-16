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
            string CSVPath = "";

            var oknoVyhladavania = new System.Windows.Forms.OpenFileDialog();
            oknoVyhladavania.Title = "Výber databázy údajov";
            oknoVyhladavania.InitialDirectory = @"c:\";
            oknoVyhladavania.Filter = "CSV (*.csv)|*.csv";
            oknoVyhladavania.FilterIndex = 2;
            oknoVyhladavania.RestoreDirectory = true;
            if (oknoVyhladavania.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                CSVPath = oknoVyhladavania.FileName;




            Connection db = new Connection();
            
            List<List<string>> zaznamCSV =new List<List<string>>();
            List<List<string>> zaznamSQL = new List<List<string>>();

            if(CSVPath != "")
            { 
                if(CSVReader(CSVPath, 3, ref zaznamCSV) > 0)
                { 
                    Console.WriteLine("VYPIS DAT Z CSV:");
                    foreach (var i in zaznamCSV)
                    {
                        foreach (var j in i)
                            Console.Write(j + " ");
                        Console.WriteLine("");
                    }
                }

                /*
                SQLiteWriter(db, "company", zaznamCSV);

                SQLiteReader(db, "company", ref zaznamSQL);

                Console.WriteLine("VYPIS DAT ZO SQL:");
                foreach (var i in zaznamSQL)
                {
                    foreach (var j in i)
                        Console.Write(j);
                    Console.WriteLine("");
                }
                */
            }
        }

        int CSVReader(string filePath, int count, ref List<List<string>> zaznam)
        {
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string riadok = reader.ReadLine();
                        var dataZRiadku = riadok.Split(';');

                        if (dataZRiadku.Length != count) // test ci je spravny pocet udajov v zazname
                        { 
                            MessageBox.Show($"Niektorý záznam obsahuje nesprávny počet polí ({count})!", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("ERROR - wrong number of records!");
                            return -1;
                        }

                        try // test ci ID udaj je cislo
                        { 
                            int.Parse(dataZRiadku[0]);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Niektorý záznam neobsahuje číselný údaj v číselnom poli!", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine(ex.ToString());
                            return -2;
                        }

                        List<string> tmp = new List<string>(); // pridanie udajov do zaznamu
                        for(int i=0;i<count;i++)
                            tmp.Add(dataZRiadku[i]);
                        zaznam.Add(tmp);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Neočakávaná chyba pri čítaní zo súboru CSV!", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.ToString());
                return -3;
            }
            return 1;
        }

        void SQLiteWriter(Connection db, string table, List<List<string>> zaznam)
        {
            db.conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(db.conn);


            if (table == "company")
            {
                foreach (var i in zaznam)
                {
                    try
                    {
                        cmd.CommandText = "INSERT INTO company(Id, Name, Address) VALUES(@id, @name, @address)";
                        cmd.Parameters.AddWithValue("@id", i[0]);
                        cmd.Parameters.AddWithValue("@name", i[1]);
                        cmd.Parameters.AddWithValue("@address", i[2]);
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex){MessageBox.Show("ERROR", "ROVNAKE ID V DATABAZE"); }
                    // POZOR NIE JE OSETRENIE PRIDAVANIE ZAZNAMU S ROVNAKYM ID, ZATIAL LEN TAKTO CEZ TRY---
                }
            }

            db.conn.Close();
        }

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
    }
}
