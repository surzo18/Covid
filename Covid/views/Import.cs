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

        List<string[]> schools = new List<string[]>(); // list o skolach...
        int poradie;

        public Import()
        {
            InitializeComponent();

            // TODO: docasne naplnenie mien skol z databazy----
            string[] a = new string[2];
            a[0] = "Stredná priemyselná škola informačných technológií"; // nazov
            a[1] = "1"; // ID
            schools.Add(a);
            string[] b = new string[2];
            b[0] = "SOU"; // nazov
            b[1] = "2"; // ID
            schools.Add(b);
            string[] c = new string[2];
            c[0] = "GYMNAZIUM"; // nazov
            c[1] = "3"; // ID
            schools.Add(c);

            // TODO: zistit posledne ID v tabulke
            poradie = 0;
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

            if(CSVPath != "")
            { 
                if(CSVReader(CSVPath, 10, ref zaznamCSV) > 0)
                { 
                    Console.WriteLine("VYPIS DAT Z CSV:");
                    foreach (var i in zaznamCSV)
                    {
                        foreach (var j in i)
                            Console.Write(j + " ");
                        Console.WriteLine("");
                    }
                }

                SQLiteWriter(db, "user", zaznamCSV);

                /*
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
            }
        }

        int CSVReader(string filePath, int count, ref List<List<string>> zaznam)
        {
            try
            {
                using (var reader = new StreamReader(filePath, Encoding.Default))
                {
                    reader.ReadLine(); // odfiltrovanie prveho riadku
                    
                    while (!reader.EndOfStream)
                    {
                        var riadok = reader.ReadLine();
                        var dataZRiadku = riadok.Split(';');

                        if (dataZRiadku.Length != count) // test ci je spravny pocet udajov v zazname
                        { 
                            MessageBox.Show($"Niektorý záznam obsahuje nesprávny počet polí ({count})!", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("ERROR - wrong number of records!");
                            return -1;
                        }

                        /*
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
                        */

                        List<string> tmp = new List<string>(); // pridanie udajov do zaznamu
                        for (int i = 0; i < count; i++)
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
                        cmd.CommandText = "INSERT INTO company(Name, Address) VALUES(@name, @address)";
                        cmd.Parameters.AddWithValue("@name", i[0]);
                        cmd.Parameters.AddWithValue("@address", i[1]);
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex){MessageBox.Show("ERROR", "ROVNAKE ID V DATABAZE"); }
                    // POZOR NIE JE OSETRENIE PRIDAVANIE ZAZNAMU S ROVNAKYM ID, ZATIAL LEN TAKTO CEZ TRY---
                }
            }
            else if (table == "user")
            {
                int role = 1; // rola ziaka

                foreach (var i in zaznam)
                {
                    try
                    {
                        var today = DateTime.Today;
                        var dateOfBirth = i[2].Split('.');
                        var a = (today.Year * 100 + today.Month) * 100 + today.Day;
                        var b = (int.Parse(dateOfBirth[2]) * 100 + int.Parse(dateOfBirth[1])) * 100 + int.Parse(dateOfBirth[0]);
                        int age = (a - b) / 10000;

                        string school = "";
                        var schoolAddress = i[7].Split(',');
                        foreach(var j in schools)
                        {
                            if (schoolAddress[0] == j[0])
                                school = j[1];
                        }

                        cmd.CommandText = "INSERT INTO user(Id, School_id, Role_id, Name, Surname, Study_year, Identification_number, Address, Phone, Email, Age, Birth_date, Year_letter) VALUES(@id, @school_id, @role_id, @name, @surname, @study_year, @identification_number, @address, @phone, @email, @age, @birth_date, @year_letter)";
                        cmd.Parameters.AddWithValue("@id", ++poradie);
                        cmd.Parameters.AddWithValue("@school_id", school);
                        cmd.Parameters.AddWithValue("@role_id", role);
                        cmd.Parameters.AddWithValue("@name", i[1]);
                        cmd.Parameters.AddWithValue("@surname", i[0]);
                        cmd.Parameters.AddWithValue("@study_year", i[8]);
                        cmd.Parameters.AddWithValue("@identification_number", i[3]);
                        cmd.Parameters.AddWithValue("@address", i[4] + ", " + i[5] + " " + i[6]);
                        cmd.Parameters.AddWithValue("@phone", "");
                        cmd.Parameters.AddWithValue("@email", "");
                        cmd.Parameters.AddWithValue("@age", age);
                        cmd.Parameters.AddWithValue("@birth_date", i[2]);
                        cmd.Parameters.AddWithValue("@year_letter", i[9]);
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex) { MessageBox.Show("ERROR", "ROVNAKE ID V DATABAZE"); }
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
