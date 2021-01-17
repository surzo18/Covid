using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covid
{
    class CustomQueries
    {
        /*
         * Vráti userov ktorý majú podobné mená
         */
        public string GetUsersByName(string name, string surname)
        {
            return "select * from user where name LIKE '" + name + "%' and surname LIKE '" + surname + "%'";
        }

        public string GetCompanyById(int id)
        {
            return "select * from company where id = " + id;
        }

        public string GetRoleById(int id)
        {
            return "select * from user_role where Id = " + id;
        }
    }
}
