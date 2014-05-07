using System.Data.Entity;

namespace Durandal_One.Models
{
    public class PersonRepository : DbContext
    {
        public DbSet<Person> Peoples { get; set; }
    }
}