using Dapper;
using NetCore.Dao;
using NetCore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Service
{
    public class Member2Service : IMember2Service
    {
        private IMember2Dao _Member2Dao;
        public Member2Service()
        {
            
        }
        public Member2Service(string ConnectionStrings)
        {
            _Member2Dao = new Member2Dao(ConnectionStrings);
        }
        public Member2Service(IMember2Dao Member2Dao)
        {
            _Member2Dao = Member2Dao;
        }

        public void SetConnectionStrings(string ConnectionStrings)
        {
            _Member2Dao= new Member2Dao(ConnectionStrings);
        }

        public IList<Member2> getAllData()
        {
            return _Member2Dao.getAllData();
        }

        public void Insert(Member2 Member2)
        {
            _Member2Dao.Insert(Member2);
        }

        public void Update(Member2 Member2)
        {
            _Member2Dao.Update(Member2);
        }

        public void Delete(string ID)
        {
            _Member2Dao.Delete(ID);
        }

        public Member2 getData(string ID)
        {
            return _Member2Dao.getData(ID);
        }
    }
}
