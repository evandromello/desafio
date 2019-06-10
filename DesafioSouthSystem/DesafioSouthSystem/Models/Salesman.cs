using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioSouthSystem.Models
{
    public class Salesman
    {
        public Salesman(string CPF, string Name, string Salary)
        {
            this.CPF = CPF;
            this.Name = Name;
            this.Salary = Salary;
        }
        private string CPF { get; set; }
        private string Name { get; set; }
        private string Salary { get; set; }
    }
}