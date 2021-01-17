
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
            guna2DataGridView1.AllowUserToAddRows = false;
        }
       
        private void Search_Load(object sender, EventArgs e)
        {
            this.refereshDatagrid();
        }


        private void refereshDatagrid()
        {
            if (this.personResults.Count <= 0) return;
            guna2DataGridView1.Rows.Add(this.personResults.Count);
            for (int i = 0; i < this.personResults.Count; i++)
            {
                guna2DataGridView1.Rows[i].Cells[0].Value = this.personResults[i].id;
                guna2DataGridView1.Rows[i].Cells[1].Value = this.personResults[i].surname + " " + this.personResults[i].name;
                guna2DataGridView1.Rows[i].Cells[2].Value = this.personResults[i].company.name;
                guna2DataGridView1.Rows[i].Cells[3].Value = this.personResults[i].id_number;
                guna2DataGridView1.Rows[i].Cells[4].Value = this.personResults[i].study_year + "." + this.personResults[i].year_letter;
                guna2DataGridView1.Rows[i].Cells[5].Value = "Pridaj";
                guna2DataGridView1.Rows[i].Cells[7].Value = "Potvrď";
            }
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
                    this.personResults.Add(new Person(
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
            this.personResults = new List<Person>();
          

            //ak je priezvisko prazdne nehladaj nič
            if(this.searchedSurname == "")
            {
                return;
            }

            //Loadne nove vyhľadávané data
            this.loadDataForGridFromDb();
            //Refreshne datagrid
            this.refereshDatagrid();

            this.searchedName = "";
            this.searchedSurname = "";
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Person clickedOne = this.personResults[e.RowIndex];
           // MessageBox.Show(clickedOne.surname);
            Fronta.SendToFront(clickedOne);


        }


        private void guna2TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                gunaCircleButton1_Click(this, new EventArgs());
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                guna2DataGridView1.Columns[6].Visible = true;
                guna2DataGridView1.Columns[7].Visible = true;
            }
            else
            {
                guna2DataGridView1.Columns[6].Visible = false;
                guna2DataGridView1.Columns[7].Visible = false;
            }
        }
    }
}
