using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covid.Models
{
    public class Person
    {
        public int id { get; private set; }
        public Company company { get; private set; } //skola
        public UserRole role { get; private set; }
        public string name { get; private set; }
        public string surname { get; private set; }
        public int study_year { get; private set; } //rocnik
        public string year_letter { get; private set; } // A B C D - 4.A
        public string id_number { get; private set; } //rodne cislo
        public string address { get; private set; }
        public string phone { get; private set; }
        public string mail { get; private set; }
        public int age { get; private set; }
        public string birth_date { get; private set; }

        public Person(int id, int school_id, int role_id, string name, string surname, int study_year, string id_number, string address, string phone, string mail, int age, string birth_date, string year_letter)
        {
            this.company = Company.getCompanyById(school_id);
            this.role = UserRole.getRoleById(role_id);
            this.name = name;
            this.surname = surname;
            this.study_year = study_year;
            this.id_number = id_number;
            this.address = address;
            this.phone = phone;
            this.mail = email;
            this.age = age;
            this.birth_date = birth_date;
            this.year_letter = year_letter;
        }

    }
}
