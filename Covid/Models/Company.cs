using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covid.Models
{
    class Company
    {
        private int id { get; set; }
        private string name { get; set; }
        private int address { get; set; }


        public Company(int id, string name, string address)
        {

        }

        public static Company getCompanyById(int id)
        {
            using (SQLiteConnection conn = new Connection().conn)
            {
                string stm = new CustomQueries().GetCompanyById(id);

                SQLiteCommand cmd = new SQLiteCommand(stm, conn);
                SQLiteDataReader rdr = cmd.ExecuteReader();
                return new Company((int)rdr["id"], rdr["name"].ToString(), rdr["address"].ToString());
                conn.Close();
            };
        }
    }
}
