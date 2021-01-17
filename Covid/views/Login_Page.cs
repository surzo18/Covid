using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Covid.Views
{
    public partial class Login_Page : Form
    {
        public static string datum;

        public Login_Page()
        {
            InitializeComponent();
            datum = DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString();
            guna2DateTimePicker1.Value = DateTime.Now;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //if (txtUserName.Text == "coviddatabaza" && txtpassword.Text == "covid")
            {
                MainMenu _load = new MainMenu(this);
                _load.Show();
                txtUserName.Clear();
                txtpassword.Clear();
            }
            /*else
            {
                MessageBox.Show("Nespravne zadane Meno alebo heslo");
                txtUserName.Clear();
                txtpassword.Clear();
                txtUserName.Focus();
            }*/
        }


        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var DTPicker = guna2DateTimePicker1.Value;
            if (DTPicker == null) 
                return;
            else
                datum = DTPicker.Day.ToString() + "." + DTPicker.Month.ToString() + "." + DTPicker.Year.ToString();
        }
    }
}
