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
    public partial class Search : Form
    {
        public Search()
        {
            InitializeComponent();
        }
       
        private void Search_Load(object sender, EventArgs e)
        {
            List<List<string>> databaza = new List<List<string>>();
            List<string> user1 = new List<string>();
            user1.Add("10."); 
            user1.Add("Jožko Mrkvička");
            user1.Add("SPŠ IT");
            user1.Add("561235/7842");
            user1.Add("III.AI");
            databaza.Add(user1);
            List<string> user2 = new List<string>();
            user2.Add("45.");
            user2.Add("Lukaš Rezetka");
            user2.Add("SPŠ IT");
            user2.Add("458732/4823");
            user2.Add("IV.BI");
            databaza.Add(user2);
            List<string> user3 = new List<string>();
            user3.Add("13.");
            user3.Add("Martin Haranta");
            user3.Add("SOUKNM");
            user3.Add("784535/7865");
            user3.Add("I.A");
            databaza.Add(user3);
            List<string> user4 = new List<string>();
            user4.Add("99.");
            user4.Add("Marek Horvát");
            user4.Add("SPŠ IT");
            user4.Add("845694/7834");
            user4.Add("I.CI");
            databaza.Add(user4);
            List<string> user5 = new List<string>();
            user5.Add("99.");
            user5.Add("Ján Štens");
            user5.Add("SPŠ IT");
            user5.Add("236578/7896");
            user5.Add("III.AG");
            databaza.Add(user5);

            guna2DataGridView1.Rows.Add(databaza.Count);
            for(int i=0;i<databaza.Count;i++)
            {
                int j = 0;
                while(j<(databaza[0].Count))
                {
                    guna2DataGridView1.Rows[i].Cells[j].Value = databaza[i][j];
                    j++;
                }
                guna2DataGridView1.Rows[i].Cells[j].Value = "Pridaj";
                guna2DataGridView1.Rows[i].Cells[++j].Value = "Možnosti";
            }
        }


    }
}
