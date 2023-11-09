using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HW_14._10._23
{
    public class Person
    {
       public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }

    public class PersonContext:DbContext
    {
        public PersonContext() : base("PersonsDb")
        { }

        public DbSet<Person> Persons { get; set; }
    }
}
