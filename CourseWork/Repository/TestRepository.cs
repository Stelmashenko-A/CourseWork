using System.Collections.Generic;
using System.Linq;
using TestData;

namespace Repository
{
    public class TestRepository : IRepository<Test>
    {
        public void Dispose()
        {
            //todo
        }

        public IQueryable<Test> GetAll(int userId)
        {
            var l = new List<Test> {new Test("1"), new Test("2"), new Test("3")};
            return l.AsQueryable();
        }

        public Test Get(int userId, int id)
        {
            return new Test(id.ToString());
        }

        public void Delete(int userId,int id)
        {
        }

        public void Save(int userId)
        {
        }

        public void Add(int userId, Test obj)
        {
        }
    }
}