using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Covid.Models;
using Covid.Views;

namespace Covid
{
    public partial class Export : Form
    {
        public Export()
        {
            InitializeComponent();
            
            // TODO: docasne pre tetovanie pridavania
            Fronta.SendToFront(new Person(1, 1, 1, "Jano", "Dudy", 1, "H9", "m", "09", "@", 19, "1999", "II.A"));
            Fronta.SendToFront(new Person(2, 1, 1, "Peto", "Tvrdy", 1, "E8", "m", "04", "@", 20, "1998", "III.S"));

        }
    }
}
