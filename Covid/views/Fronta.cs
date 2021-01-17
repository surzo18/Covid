using Covid.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Covid
{
    public partial class Fronta : Form
    {
        public Fronta()
        {
            InitializeComponent();
        }
        
        public void SendToFront(Person p)
        {

        }

        private void Fronta_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Add(2);
            guna2DataGridView1.Rows[0].Cells[0].Value = "1.";
            guna2DataGridView1.Rows[0].Cells[1].Value = "Jožko Mrkvička";
            guna2DataGridView1.Rows[0].Cells[2].Value = "SPŠ IT";
            guna2DataGridView1.Rows[0].Cells[3].Value = "561235/7842";
            guna2DataGridView1.Rows[0].Cells[4].Value = "III.AI";
            guna2DataGridView1.Rows[0].Cells[6].Value = "Potvrď";
            
            guna2DataGridView1.Rows[1].Cells[0].Value = "45.";
            guna2DataGridView1.Rows[1].Cells[1].Value = "Lukaš Rezetka";
            guna2DataGridView1.Rows[1].Cells[2].Value = "SPŠ IT";
            guna2DataGridView1.Rows[1].Cells[3].Value = "458732/4823";
            guna2DataGridView1.Rows[1].Cells[4].Value = "IV.BI";
            guna2DataGridView1.Rows[1].Cells[6].Value = "Potvrď";
            
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
