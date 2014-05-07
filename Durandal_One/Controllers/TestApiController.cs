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
        private readonly PersonRepository _personRep = new PersonRepository();

        [HttpGet]
        public IEnumerable<Person> GetAllPersons()
        {
            return _personRep.Peoples;
        }

        [HttpGet]
        public Person GetPerson(int id)
        {
            var person = _personRep.Peoples.Find(id);
            if (person != null)
            {
                return person;
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        [HttpPost]
        public HttpResponseMessage PostPerson(Person person)
        {
            _personRep.Peoples.Add(person);
            _personRep.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpDelete]
        public void DeletePerson(int id)
        {
            var personToDelete = _personRep.Peoples.First(q => q.PersonId.Equals(id));
            if (personToDelete != null)
            {
                _personRep.Peoples.Remove(personToDelete);
                _personRep.SaveChanges();
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [HttpPut]
        public void PutPerson(int id, Person person)
        {
            var found = _personRep.Peoples.Find(id);
            if (found != null)
            {
                person.PersonId = id;
                _personRep.Entry(found).CurrentValues.SetValues(person);
                _personRep.SaveChanges();
            }

        }
    }
}
