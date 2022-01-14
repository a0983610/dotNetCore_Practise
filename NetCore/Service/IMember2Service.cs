using NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Service
{
    public interface IMember2Service
    {
        public void SetConnectionStrings(string ConnectionStrings);
        public IList<Member2> getAllData();

        public Member2 getData(string ID);

        public void Insert(Member2 Member2);
        public void Update(Member2 Member2);
        public void Delete(string ID);
    }
}
