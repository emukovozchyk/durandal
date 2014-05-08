using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Durandal_One.Models
{
    [DataContract]
    public class Person
    {
        [DataMember]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [DataMember]
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }

        [DataMember(IsRequired = true)]
        [Required]
        [Range(18, 99)]
        public int Age { get; set; }

        public Person()
        {
        }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}