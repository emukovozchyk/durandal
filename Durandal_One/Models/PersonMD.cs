using MongoDB.Bson;

namespace Durandal_One.Models
{
    public class PersonMD : Person
    {
        public PersonMD(int id, string name, int age) : base(id, name, age)
        {
        }

        public ObjectId Id { get; set; }

        public PersonMD()
        {
        }
    }
}