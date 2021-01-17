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

        public UserRole()
        {

        }

        public static UserRole getRoleById(int id)
        {
            UserRole newUserRole = new UserRole();
            using (SQLiteConnection conn = new Connection().conn)
            {
                conn.Open();
                string stm = new CustomQueries().GetRoleById(id);

                SQLiteCommand cmd = new SQLiteCommand(stm, conn);
                SQLiteDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read()){
                    newUserRole = new UserRole(
                        Convert.ToInt32(rdr["Id"]), 
                        rdr["Role_Name"].ToString()
                        );
                }

                conn.Close();
                return newUserRole;
            };

        }
    }
}
