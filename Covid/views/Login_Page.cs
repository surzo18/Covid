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
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            MainMenu _load = new MainMenu();
            this.Hide(); _load.Show();
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
