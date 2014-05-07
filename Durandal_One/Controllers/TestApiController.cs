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
        readonly MongoCollection<Person> _collection = Db.GetCollection<Person>("people");


        [HttpGet]
        public IEnumerable<Person> GetAllPersons()
        {
            var query = (from e in _collection.AsQueryable() select e).ToList();
            return query;
        }

        [HttpGet]
        public Person GetPerson(string id)
        {
            var query = Query<Person>.EQ(e => e.Id, id);
            var p = _collection.FindOne(query);
            if (p != null)
            {
                return p;
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        [HttpPost]
        public HttpResponseMessage PostPerson(Person person)
        {
            _collection.Insert(person);
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpDelete]
        public void DeletePerson(string id)
        {
            var query = Query<Person>.EQ(e => e.Id, id);
            _collection.Remove(query);
        }

        [HttpPut]
        public void PutPerson(string id, Person person)
        {
            var query = Query<Person>.EQ(e => e.Id, id);
            var personExisting = _collection.FindOne(query);
            personExisting.Name = person.Name;
            personExisting.Age = person.Age;

            _collection.Save(personExisting);
        }
    }
}
