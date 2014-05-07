using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Durandal_One.Models;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Durandal_One.Controllers
{
    public class TestApiController : ApiController
    {
        private static readonly MongoClient Client = new MongoClient("mongodb://testDurandal:test@ds063178.mongolab.com:63178/appharbor_b0e0c4d0-2c7c-4170-aff1-7b173d648a99");
        private static readonly MongoServer Server = Client.GetServer();
        private static readonly MongoDatabase Db = Server.GetDatabase("appharbor_b0e0c4d0-2c7c-4170-aff1-7b173d648a99");
        readonly MongoCollection<PersonMD> _collection = Db.GetCollection<PersonMD>("people");

        [HttpGet]
        public IEnumerable<Person> GetAllPersons()
        {
            var query = from e in _collection.AsQueryable() select e;
            var people = query.Select(p => new Person(p.PersonId, p.Name, p.Age)).ToList();
            return people;
        }

        [HttpGet]
        public Person GetPerson(int id)
        {
            var query = Query<PersonMD>.EQ(e => e.PersonId, id);
            var p = _collection.FindOne(query);
            if (p != null)
            {
                return new Person(p.PersonId, p.Name, p.Age);
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        [HttpPost]
        public HttpResponseMessage PostPerson(Person person)
        {
            var max = (from e in _collection.AsQueryable() orderby e.PersonId descending select e.PersonId).FirstOrDefault();
            var pToAdd = new PersonMD(max+1, person.Name, person.Age);
            _collection.Insert(pToAdd);
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpDelete]
        public void DeletePerson(int id)
        {
            var query = Query<PersonMD>.EQ(e => e.PersonId, id);
            _collection.Remove(query);
        }

        [HttpPut]
        public void PutPerson(int id, Person person)
        {
            var query = Query<PersonMD>.EQ(e => e.PersonId, id);
            var personExisting = _collection.FindOne(query);
            personExisting.Name = person.Name;
            personExisting.Age = person.Age;

            _collection.Save(personExisting);
        }
    }
}
