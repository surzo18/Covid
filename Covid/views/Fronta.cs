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

        private void Fronta_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Add(5);
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

            guna2DataGridView1.Rows[2].Cells[0].Value = "13.";
            guna2DataGridView1.Rows[2].Cells[1].Value = "Martin Haranta";
            guna2DataGridView1.Rows[2].Cells[2].Value = "SOUKNM";
            guna2DataGridView1.Rows[2].Cells[3].Value = "784535/7865";
            guna2DataGridView1.Rows[2].Cells[4].Value = "I.A";
            guna2DataGridView1.Rows[2].Cells[6].Value = "Potvrď";

            guna2DataGridView1.Rows[3].Cells[0].Value = "99.";
            guna2DataGridView1.Rows[3].Cells[1].Value = "Marek Horvát";
            guna2DataGridView1.Rows[3].Cells[2].Value = "SPŠ IT";
            guna2DataGridView1.Rows[3].Cells[3].Value = "845694/7834";
            guna2DataGridView1.Rows[3].Cells[4].Value = "I.CI";
            guna2DataGridView1.Rows[3].Cells[6].Value = "Potvrd";

            guna2DataGridView1.Rows[4].Cells[0].Value = "99.";
            guna2DataGridView1.Rows[4].Cells[1].Value = "Ján Štens";
            guna2DataGridView1.Rows[4].Cells[2].Value = "SPŠ IT";
            guna2DataGridView1.Rows[4].Cells[3].Value = "236578/7896";
            guna2DataGridView1.Rows[4].Cells[4].Value = "III.AG";
            guna2DataGridView1.Rows[4].Cells[6].Value = "Potvrď";
        }

       
    }
}
