namespace Durandal_One.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Person()
        {
        }

        public Person(int personId, string name, int age)
        {
            PersonId = personId;
            Name = name;
            Age = age;
        }
    }
}