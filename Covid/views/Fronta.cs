﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Covid.Models;
using Guna.UI.WinForms;

namespace Covid
{
    public partial class Fronta : Form
    {
        static List<Person> personsInFront = new List<Person>();
        Connection db = new Connection();

        static Guna.UI.WinForms.GunaAdvenceButton btnFronta;
        static Label lblInfo;

        public Fronta(object obj)
        {
            InitializeComponent();
            btnFronta = (obj as Form).Controls[2].Controls[2] as Guna.UI.WinForms.GunaAdvenceButton; // fronta tlacidlo
            lblInfo = (obj as Form).Controls[2].Controls[0] as Label; // info label
        }

        public static void SendToFront(Person p, object o)
        {
            foreach (var i in personsInFront)
            {
                if (p.id == i.id)
                {
                    MessageBox.Show(p.surname + " " + p.name + " už je vo fronte. Nepodarilo sa pridať.");
                    return;
                }
            } 
            
            personsInFront.Add(p);
            MessageBox.Show(p.surname + " " + p.name + " bol pridaný do fronty.");
            
            if(btnFronta == null)
                btnFronta = (o as Form).Controls[2].Controls[2] as Guna.UI.WinForms.GunaAdvenceButton; // fronta tlacidlo
            btnFronta.Text = "Fronta (" + personsInFront.Count.ToString() + ")";
        }

        private void Fronta_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Clear();

            if (personsInFront.Count != 0)
            {
                guna2DataGridView1.Rows.Add(personsInFront.Count);

                for (int i = 0; i < personsInFront.Count; i++)
                {
                    guna2DataGridView1.Rows[i].Cells[0].Value = personsInFront[i].id;
                    guna2DataGridView1.Rows[i].Cells[1].Value = personsInFront[i].name + " " + personsInFront[i].surname;
                    guna2DataGridView1.Rows[i].Cells[2].Value = personsInFront[i].company.name;
                    guna2DataGridView1.Rows[i].Cells[3].Value = personsInFront[i].id_number;
                    guna2DataGridView1.Rows[i].Cells[4].Value = personsInFront[i].year_letter;
                    guna2DataGridView1.Rows[i].Cells[5].Value = "";
                    guna2DataGridView1.Rows[i].Cells[6].Value = "Potvrď";
                }
            }

            guna2DataGridView1.AllowUserToAddRows = false;
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int userStatusId = 0; // 0-neurčený 1-pozitivny 2-negativny
                string userStatus = guna2DataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                if (userStatus == "")
                {
                    MessageBox.Show($"Musíte zvoliť stav Pozitívny/Negatívny", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (userStatus == "Pozitívny")
                        userStatusId = 1;
                    else if (userStatus == "Negatívny")
                        userStatusId = 2;

                    DialogResult akcept = MessageBox.Show($"Určite chcete potvrdiť užívateľa?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (akcept == DialogResult.Yes)
                    {
                        SQLiteWriterTesting(personsInFront[e.RowIndex], userStatusId);
                        personsInFront.RemoveAt(e.RowIndex);
                        guna2DataGridView1.Rows.RemoveAt(e.RowIndex);

                        btnFronta.Text = "Fronta (" + personsInFront.Count.ToString() + ")";

                        // ZAPIS UDAJOV DO INFOPANELU
                        lblInfo.Text = "počet testovaných\n" + (Connection.CountOfTesting()).ToString() + "/" + (Connection.CountOfUser()).ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Neočakávaná chyba pri potvrdení užívateľa!", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.ToString());
                return;
            }
        }

        void SQLiteWriterTesting(Person p, int status)
        {
            db.conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(db.conn);

            // ZAPIS DO DATABAZY
            try
            {
                cmd.CommandText = "INSERT INTO testing(User_id, Testing_date, Is_negative) VALUES(@user_id, @testing_date, @is_negative)";
                cmd.Parameters.AddWithValue("@user_id", p.id);
                cmd.Parameters.AddWithValue("@testing_date", Views.Login_Page.datum);
                cmd.Parameters.AddWithValue("@is_negative", status);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Neočakávaná chyba pri zápise do databázy!", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.ToString());
                db.conn.Close();
                return;
            }

            db.conn.Close();
        }
    }
}
