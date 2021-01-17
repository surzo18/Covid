
﻿using Covid.Models;
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

namespace Covid
{
    public partial class Search : Form
    {
        private string searchedSurname;
        private string searchedName;
        private List<Person> personResults;

        public Search()
        {
            InitializeComponent();
            this.personResults = new List<Person>();
        }
       
        private void Search_Load(object sender, EventArgs e)
        {
            /*
            List<List<string>> databaza = new List<List<string>>();
            List<string> user1 = new List<string>();
            user1.Add("10."); 
            user1.Add("Jožko Mrkvička");
            user1.Add("SPŠ IT");
            user1.Add("561235/7842");
            user1.Add("III.AI");
            databaza.Add(user1);
            List<string> user2 = new List<string>();
            user2.Add("45.");
            user2.Add("Lukaš Rezetka");
            user2.Add("SPŠ IT");
            user2.Add("458732/4823");
            user2.Add("IV.BI");
            databaza.Add(user2);
            List<string> user3 = new List<string>();
            user3.Add("13.");
            user3.Add("Martin Haranta");
            user3.Add("SOUKNM");
            user3.Add("784535/7865");
            user3.Add("I.A");
            databaza.Add(user3);
            List<string> user4 = new List<string>();
            user4.Add("99.");
            user4.Add("Marek Horvát");
            user4.Add("SPŠ IT");
            user4.Add("845694/7834");
            user4.Add("I.CI");
            databaza.Add(user4);
            List<string> user5 = new List<string>();
            user5.Add("99.");
            user5.Add("Ján Štens");
            user5.Add("SPŠ IT");
            user5.Add("236578/7896");
            user5.Add("III.AG");
            databaza.Add(user5);

            guna2DataGridView1.Rows.Add(databaza.Count);
            for(int i=0;i<databaza.Count;i++)
            {
                int j = 0;
                while(j<(databaza[0].Count))
                {
                    guna2DataGridView1.Rows[i].Cells[j].Value = databaza[i][j];
                    j++;
                }
                guna2DataGridView1.Rows[i].Cells[j].Value = "Pridaj";
            }
            */
        }


        private void parseInputFromSearch(string input)
        {
            var input_parts = input.Split(' ');
            this.searchedSurname= input_parts[0];

            if (input_parts.Length == 2)
            {
                this.searchedName = input_parts[1];
            }
        }

        private void loadDataForGridFromDb()
        {
            //Pripojenie do db
            using(SQLiteConnection conn = new Connection().conn)
            {
                conn.Open();
                string stm = new CustomQueries().GetUsersByName(this.searchedName, this.searchedSurname);

                SQLiteCommand cmd = new SQLiteCommand(stm, conn);
                SQLiteDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    //MessageBox.Show(rdr["Id"].ToString());
                    //Console.WriteLine(rdr["Name"]);

                    int id = Convert.ToInt32(rdr["Id"]);
                    int schoold_id = Convert.ToInt32(rdr["School_id"]);
                    int role_id = Convert.ToInt32(rdr["Role_id"]);
                    string name = rdr["Name"].ToString();
                    string surname = rdr["Surname"].ToString();
                    int study_year = Convert.ToInt32(rdr["Study_year"]);
                    string id_number = rdr["Identification_number"].ToString();
                    string address = rdr["Address"].ToString();
                    string phone = rdr["Phone"].ToString();
                    string email = rdr["Email"].ToString();
                    int age = Convert.ToInt32(rdr["Age"]);
                    string birth_date = rdr["Birth_date"].ToString();
                    string year_letter = rdr["Year_letter"].ToString();
                    this.personResults.Append(new Person(
                        id,schoold_id,role_id,name,surname,study_year,id_number,address,phone,email,age,birth_date,year_letter
                        ));
                }
                conn.Close();
            };
            
        }

        private void gunaCircleButton1_Click(object sender, EventArgs e)
        {
            //nastavi hladane na aktualnu hodnotu
            this.parseInputFromSearch(guna2TextBox1.Text);

            //Clearne grid vysledkov
            guna2DataGridView1.Rows.Clear();

            //ak je priezvisko prazdne nehladaj nič
            if(this.searchedSurname == "")
            {
                return;
            }

            //Loadne nove vyhľadávané data
            this.loadDataForGridFromDb();
            //
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
      

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                guna2DataGridView1.Columns[6].Visible = true;
            }
            else
            {
                guna2DataGridView1.Columns[6].Visible = false;
            }
        }

        private void guna2TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                gunaCircleButton1_Click(this, new EventArgs());
            }
        }

    }
}
