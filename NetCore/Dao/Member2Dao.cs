using Dapper;
using NetCore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Dao
{
    public class Member2Dao : IMember2Dao
    {
        private string _ConnectionStrings = "";

        public Member2Dao(string ConnectionStrings)
        {
            _ConnectionStrings = ConnectionStrings;
        }

        public IList<Member2> getAllData()
        {
            using (SqlConnection con = new SqlConnection(_ConnectionStrings))
            {
                var data = con.Query<Member2>(@"SELECT [Id]
                                                      ,[Name]
                                                      ,[Phone]
                                                      ,[Tel]
                                                      ,[Gender]
                                                      ,[Birthday] from Member2").ToList();
                return data;
            }
        }

        public void Insert(Member2 model)
        {
            using (SqlConnection con = new SqlConnection(_ConnectionStrings))
            {
                con.Execute(@"
            INSERT INTO [dbo].[Member2]
                   ([Id]
                   ,[Name]
                   ,[Phone]
                   ,[Tel]
                   ,[Gender]
                   ,[Birthday])
             VALUES
                   (@ID
                   ,@Name
                   ,@Phone
                   ,@Tel
                   ,@Gender
                   ,@Birthday)",
           new
           {
               ID = Guid.NewGuid(),
               model.Name,
               model.Phone,
               model.Tel,
               model.Gender,
               model.Birthday
           });


            }
        }

        public void Update(Member2 model)
        {
            using (SqlConnection con = new SqlConnection(_ConnectionStrings))
            {
                con.Execute(@"
UPDATE [dbo].[Member2]
   SET [Name] = @Name
      ,[Phone] = @Phone
      ,[Tel] = @Tel
      ,[Gender] = @Gender
      ,[Birthday] = @Birthday
 WHERE Id=@ID",
 new
 {
     model.ID,
     model.Name,
     model.Phone,
     model.Tel,
     model.Gender,
     model.Birthday
 });

            }
        }

        public void Delete(string ID)
        {
            using (SqlConnection con = new SqlConnection(_ConnectionStrings))
            {
                con.Execute(@"DELETE FROM [dbo].[Member2]
      WHERE Id=@id", new { id = ID });

            }
        }

        public Member2 getData(string ID)
        {
            using (SqlConnection con = new SqlConnection(_ConnectionStrings))
            {
                var data = con.Query<Member2>(@"SELECT [Id]
                                                      ,[Name]
                                                      ,[Phone]
                                                      ,[Tel]
                                                      ,[Gender]
                                                      ,[Birthday] from Member2 where Id=@ID", new { ID }).FirstOrDefault();
                return data;
            }
        }
    }
}
