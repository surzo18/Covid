using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covid.Models
{
    public class Company
    {
        public int id { get; private set; }
        public string name { get; private set; }
        public string address { get; private set; }


        public Company(int id, string name, string address)
        {
            this.id = id;
            this.name = name;
            this.address = address;
        } 

        public static Company getCompanyById(int id)
        {
            using (SQLiteConnection conn = new Connection().conn)
            {
                conn.Open();
                string stm = new CustomQueries().GetCompanyById(id);

                SQLiteCommand cmd = new SQLiteCommand(stm, conn);
                SQLiteDataReader rdr = cmd.ExecuteReader();
                Company tmp = new Company((int)rdr["id"], rdr["name"].ToString(), rdr["address"].ToString()); // kvoli poradiu uzatvorenia SPOJENIA a RETURNU
                conn.Close();
                return tmp;
            };
        }
    }
}
