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
        static List<Person> personsInFront = new List<Person>();
        public Fronta()
        {
            InitializeComponent();
            SendToFront(new Person(1, 1, 1, "Jano", "Dudy", 1, "H9", "m", "09", "@", 19, "1999", "II.A"));
            SendToFront(new Person(2, 1, 1, "Peto", "Tvrdy", 1, "E8", "m", "04", "@", 20, "1998", "III.S"));
        }
        
        public void SendToFront(Person p)
        {
            personsInFront.Add(p);
        }

        private void Fronta_Load(object sender, EventArgs e)
        {
            ShowFront();
        }

        void ShowFront()
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
                    guna2DataGridView1.Rows[i].Cells[6].Value = "Potvrď";
                }
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
