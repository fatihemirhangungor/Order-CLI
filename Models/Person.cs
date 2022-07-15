using System.Collections.Generic;
using System;


namespace Order_CLI.Models
{
    class Person
    {
        public string Tc { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public List<Product> Products { get; set; }

        public Person(string tc, string name, string surname, string phone, List<Product> products)
        {
            Tc = tc;
            Name = name;
            Surname = surname;
            Phone = phone;
            Products = products;
        }
    }
}
