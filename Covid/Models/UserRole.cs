using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covid.Models
{
    class UserRole
    {
        private int id { get; set; }
        private string userRoleName { get; set; }
        
        public UserRole(int id, string userRoleName )
        {
            this.id = id;
            this.userRoleName = userRoleName;
        }

        public static UserRole getRoleById(int id)
        {
            using (SQLiteConnection conn = new Connection().conn)
            {
                string stm = new CustomQueries().GetCompanyById(id);

                SQLiteCommand cmd = new SQLiteCommand(stm, conn);
                SQLiteDataReader rdr = cmd.ExecuteReader();
                return new UserRole(
                    (int)rdr["id"], 
                    rdr["name"].ToString()
                    );
                conn.Close();
            };
        }
    }
}
