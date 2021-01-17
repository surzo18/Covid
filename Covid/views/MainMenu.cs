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

namespace Covid.Views
{
    public partial class MainMenu : Form
    {
        Form login;

        public MainMenu( Form login)
        {
            InitializeComponent();
            this.login = login;
            this.login.Hide();

            // VYPISOVANIE INFO O TESTOVANYCH
            int testovanych = 0;
            int vsetkych = 0;
            try
            {
                Connection db = new Connection();
                db.conn.Open();
                string stm = "SELECT * FROM testing LIMIT 10000"; // TENTO LIMIT TREBA ZVAZIT!
                SQLiteCommand cmd = new SQLiteCommand(stm, db.conn);
                SQLiteDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    testovanych++;
                stm = "SELECT * FROM user LIMIT 10000"; // TENTO LIMIT TREBA ZVAZIT!
                cmd = new SQLiteCommand(stm, db.conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    vsetkych++;
                db.conn.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Probem s databazou!");
                Console.WriteLine(ex.ToString());
            }
            pocetLudi.Text = "počet testovaných\n" + testovanych.ToString() + "/" + vsetkych.ToString();



        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            guna2PictureBox_val.Image = Properties.Resources.icons8_search_property_32;
            container(new Search());
        }

        private void container(object _form)
        {
            if (guna2Panel_container.Controls.Count > 0) guna2Panel_container.Controls.Clear();

            Form fm = _form as Form;
            fm.TopLevel = false;
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.Dock = DockStyle.Fill;
            guna2Panel_container.Controls.Add(fm);
            guna2Panel_container.Tag = fm;
            fm.Show();

        }
       
        private void guna2Panel_top_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            guna2PictureBox_val.Image = Properties.Resources.icons8_form_32;
            container(new Fronta());
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            guna2PictureBox_val.Image = Properties.Resources.icons8_download_from_the_cloud_32;
            container(new Import());
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            guna2PictureBox_val.Image = Properties.Resources.icons8_upload_to_the_cloud_32;
            container(new Export());
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            login.Close();
            this.Close();
        }

    

        private void gunaAdvenceButton5_Click_1(object sender, EventArgs e)
        {
            login.Show();
            this.Close();
        }
    }
}
