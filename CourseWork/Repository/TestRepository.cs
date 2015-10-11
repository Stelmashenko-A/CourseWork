using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Document;
using TestData;

namespace Repository
{
    public class TestRepository : IRepository<Test>
    {
        public void Dispose()
        {
            //todo
        }

        public IQueryable<Test> GetAll(long userId)
        {
            var l = new List<Test> {new Test("1"), new Test("2"), new Test("3")};
            return l.AsQueryable();
        }

        public Test Get(long userId, long id)
        {
            return new Test(id.ToString());
        }

        public void Delete(long userId, long id)
        {
        }

        public void Save(long userId)
        {
        }

        public void Add(long userId, Test obj)
        {
            using (IDocumentStore store = new DocumentStore
            {
                Url = "http://localhost:8081/", // server URL
                DefaultDatabase = "Test"   // default database
            })
            {
                store.Initialize(); // initializes document store, by connecting to server and downloading various configurations

                using (IDocumentSession session = store.OpenSession()) // opens a session that will work in context of 'DefaultDatabase'
                {
                   
                    session.Store(obj); // stores employee in session, assigning it to a collection `Employees`
                   // string employeeId = 1.ToString(); // Session.Store will assign Id to employee, if it is not set

                    session.SaveChanges(); // sends all changes to server

                    // Session implements Unit of Work pattern,
                    // therefore employee instance would be the same and no server call will be made
                    //Test loadedEmployee = session.Load<Test>(1);
                   // Assert.Equal(employee, loadedEmployee);
                }
            }
        }
    }
}