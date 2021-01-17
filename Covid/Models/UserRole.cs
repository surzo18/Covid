using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covid.Models
{
    public class UserRole
    {
        public int id { get; private set; }
        public string userRoleName { get; private set; }
        
        public UserRole(int id, string userRoleName )
        {
            this.id = id;
            this.userRoleName = userRoleName;
        }

        public static UserRole getRoleById(int id)
        {
            using (SQLiteConnection conn = new Connection().conn)
            {
                conn.Open();
                string stm = new CustomQueries().GetCompanyById(id);

                SQLiteCommand cmd = new SQLiteCommand(stm, conn);
                SQLiteDataReader rdr = cmd.ExecuteReader();
                UserRole tmp = new UserRole(
                    (int)rdr["id"],
                    rdr["name"].ToString()
                    );
                conn.Close();
                return tmp;
            };
        }
    }
}
