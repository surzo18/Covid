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
using Covid.Models;
using Covid.Views;

namespace Covid
{
    public partial class Import : Form
    {
        Label lblInfo;
        public Import(object obj)
        {
            InitializeComponent();

            lblInfo = (obj as Form).Controls[2].Controls[0] as Label; // info label

            g2b_import.Text = "Vyhľadať súbor";
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

            List<List<string>> zaznamCSV = new List<List<string>>();

            if (CSVPath != "")
            {
                g2b_import.Text = "Načítava sa...";

                // NACITANIE OBSAHU CSV SUBORA
                if (CSVReader(CSVPath, 10, ref zaznamCSV) > 0)
                {
                    /*
                    Console.WriteLine("VYPIS DAT Z CSV:");
                    foreach (var i in zaznamCSV)
                    {
                        foreach (var j in i)
                            Console.Write(j + " ");
                        Console.WriteLine("");
                    }
                    */
                }
                else
                {
                    Console.WriteLine("Chyba pri čítaní CSV súboru");
                }

                // ZAPIS DO DATABAZY
                SQLiteWriter(zaznamCSV);
            }

            g2b_import.Text = "Úspešné načítanie";

            // ZAPIS UDAJOV DO INFOPANELU
            lblInfo.Text = "počet testovaných\n" + (Connection.CountOfTesting()).ToString() + "/" + (Connection.CountOfUser()).ToString();
        }

        int CSVReader(string filePath, int count, ref List<List<string>> zaznam)
        {
            int counter = 0; // pocitadlo zaznamu kvoli lahsiemu identifikovaniu zleho zaznamu
            try
            {
                using (var reader = new StreamReader(filePath, Encoding.Default))
                {
                    reader.ReadLine(); // odfiltrovanie prveho riadku

                    while (!reader.EndOfStream)
                    {
                        counter++;
                        var riadok = reader.ReadLine();
                        var dataZRiadku = riadok.Split(';');

                        if (dataZRiadku.Length != count) // test ci je spravny pocet udajov v zazname
                        {
                            MessageBox.Show($"Niektorý záznam obsahuje nesprávny počet polí ({counter})!", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine($"ERROR - wrong number of data in records ({counter})!");
                            return -1;
                        }

                        List<string> tmp = new List<string>(); // pridanie udajov do zaznamu
                        for (int i = 0; i < count; i++)
                            tmp.Add(dataZRiadku[i]);
                        zaznam.Add(tmp);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Neočakávaná chyba pri čítaní zo súboru CSV ({counter})!", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.ToString());
                return -2;
            }
            return 1;
        }

        void SQLiteWriter(List<List<string>> zaznam)
        {
            // TODO: zatial mame len zapis ziakov , preto natvrdo rola ziaka
            int role = 1; // rola ziaka

            Connection db = new Connection();
            db.conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(db.conn);

            int errorId = 0; // zapisanie si cisla zaznamu pre lahsie najdenie chyby / DEBUG
            int resultOfDuplicate = 0;
            var today = DateTime.Today; // aktualny datum
            int age = 0; // vek ziaka
            int company = 0; // id organizacie
            int studyOfYear = 0; // rocnik studia
            List<string[]> schools = new List<string[]>();

            // VYTVORENIE ZOZNAMU ORGANIZACII
            try
            { 
                string stm = "SELECT * FROM company LIMIT 1000"; // TODO: prediskutovat limit zaznamov (1000)
                SQLiteCommand tmpCmd = new SQLiteCommand(stm, db.conn);
                SQLiteDataReader rdr = tmpCmd.ExecuteReader();
                while (rdr.Read())
                {
                    string[] tmp = new string[2];
                    tmp[0] = rdr["id"].ToString(); // ekvivalnet rdr.GetInt32(0);
                    tmp[1] = rdr["name"].ToString(); // ekvivalnet rdr.GetString(1);
                    schools.Add(tmp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba pri načítaní organizácií z databázy!", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.ToString());
                db.conn.Close();
                return;
            }

            // ZAPIS DO DATABAZY UZIVATELOV
            foreach (var i in zaznam)
            {
                errorId++;

                // IGNOROVANIE ZAZNAMU ROVNAKEHO ZIAKA
                try
                {
                    string stm = $"SELECT EXISTS(SELECT 1 FROM user WHERE Identification_number = \"{i[3]}\")";
                    SQLiteCommand tmpCmd = new SQLiteCommand(stm, db.conn);
                    SQLiteDataReader rdr = tmpCmd.ExecuteReader();
                    while (rdr.Read())
                        resultOfDuplicate = rdr.GetInt32(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Chyba pri určovaní zhody záznamu ({errorId})!", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.ToString());
                    db.conn.Close();
                    return;
                }
                if (resultOfDuplicate == 1)
                    continue;
    
                // URCENIE VEKU UZIVATELA ZO ZAZNAMU
                try
                {
                    var dateOfBirth = i[2].Split('.');
                    var a = (today.Year * 100 + today.Month) * 100 + today.Day;
                    var b = (int.Parse(dateOfBirth[2]) * 100 + int.Parse(dateOfBirth[1])) * 100 + int.Parse(dateOfBirth[0]);
                    age = (a - b) / 10000;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Chyba pri výpočte veku ({errorId})!", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.ToString());
                    db.conn.Close();
                    return;
                }

                // URCENIE TYPU ORGANIZACIE ZO ZAZNAMU
                try
                {
                    var schoolAddress = i[7].Split(',');
                    foreach (var j in schools)
                    {
                        if (schoolAddress[0] == j[1])
                        {
                            company = int.Parse(j[0]);
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Chyba pri určovaní organizácie ({errorId})!", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.ToString());
                    db.conn.Close();
                    return;
                }

                try
                {
                    studyOfYear = int.Parse(i[8]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Chyba pri určovaní ročníka štúdia ({errorId})!", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.ToString());
                    db.conn.Close();
                    return;
                }

                // ZAPIS DAT DO DATABAZY USER
                try
                {
                    cmd.CommandText = "INSERT INTO user(School_id, Role_id, Name, Surname, Study_year, Identification_number, Address, Phone, Email, Age, Birth_date, Year_letter) VALUES(@school_id, @role_id, @name, @surname, @study_year, @identification_number, @address, @phone, @email, @age, @birth_date, @year_letter)";
                    cmd.Parameters.AddWithValue("@school_id", company);
                    cmd.Parameters.AddWithValue("@role_id", role);
                    cmd.Parameters.AddWithValue("@name", i[1]);
                    cmd.Parameters.AddWithValue("@surname", i[0]);
                    cmd.Parameters.AddWithValue("@study_year", studyOfYear);
                    cmd.Parameters.AddWithValue("@identification_number", i[3]);
                    cmd.Parameters.AddWithValue("@address", i[4] + ", " + i[5] + " " + i[6]); // TODO: nevhodne pre filtrovanie
                    cmd.Parameters.AddWithValue("@phone", "");
                    cmd.Parameters.AddWithValue("@email", "");
                    cmd.Parameters.AddWithValue("@age", age);
                    cmd.Parameters.AddWithValue("@birth_date", i[2]);
                    cmd.Parameters.AddWithValue("@year_letter", i[9]);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Neočakávaná chyba pri zápise do databázy ({errorId})!", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.ToString());
                    db.conn.Close();
                    return;
                }
            }
            db.conn.Close();
        }
    }
}
