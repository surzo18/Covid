﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covid.Models
{
    public class Person
    {
        private int id { get; set; }
        private Company company { get; set; } //skola
        private UserRole role { get; set; }
        private string name { get; set; }
        private string surname { get; set; }
        private int study_year { get; set; } //rocnik
        private string year_letter { get; set; } // A B C D - 4.A
        private string id_number { get; set; } //rodne cislo
        private string address { get; set; }
        private string phone { get; set; }
        private string mail { get; set; }
        private int age { get; set; }
        private string birth_date { get; set; }

        public Person(int id, int school_id, int role_id, string name, string surname, int study_year, string id_number, string address, string phone, string email, int age, string birth_date, string year_letter)
        {
            this.company = Company.getCompanyById(school_id);
            this.role = UserRole.getRoleById(role_id);
        }

    }
}
