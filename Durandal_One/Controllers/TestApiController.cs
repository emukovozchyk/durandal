using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Durandal_One.Models;

namespace Durandal_One.Controllers
{
    public class TestApiController : ApiController
    {
        //private readonly PersonRepository _personRep = new PersonRepository();

        [HttpGet]
        public IEnumerable<Person> GetAllPersons()
        {
            return OfflineRep.ListOfPerson;
        }

        [HttpGet]
        public Person GetPerson(int id)
        {
            var person = OfflineRep.ListOfPerson.Find(q=>q.PersonId.Equals(id));
            if (person != null)
            {
                return person;
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        [HttpPost]
        public HttpResponseMessage PostPerson(Person person)
        {
            var lastId = OfflineRep.ListOfPerson.Max(q => q.PersonId);
            person.PersonId = lastId + 1;
            OfflineRep.ListOfPerson.Add(person);
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpDelete]
        public void DeletePerson(int id)
        {
            var personToDelete = OfflineRep.ListOfPerson.Find(q => q.PersonId.Equals(id));
            if (personToDelete != null)
            {
                OfflineRep.ListOfPerson.Remove(personToDelete);
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [HttpPut]
        public void PutPerson(int id, Person person)
        {
            var found = OfflineRep.ListOfPerson.Find(q=>q.PersonId.Equals(id));
            if (found != null)
            {
                found.Name = person.Name;
                found.Age = person.Age;
            }

        }
    }
}
