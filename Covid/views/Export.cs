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
            
            // TODO: docasne pre tetovanie pridavania do fronty, .... casom zmazat....
           // Fronta.SendToFront(new Person(10, 1, 1, "Jano", "Dudy", 2, "H9", "nabrezna", "0944", "a@a.a", 40, "1938", "II.A"));
           // Fronta.SendToFront(new Person(20, 1, 1, "Emil", "Virdzo", 3, "E8", "hurbanka", "0918", "ss@ss.ss", 80, "1914", "III.S"));
        }
    }
}
