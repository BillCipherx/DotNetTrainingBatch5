using DotNetTrainingBatch5.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.ConsoleApp
{
    public class AdoDotNetExample2
    {
        private readonly string _connectionString = "Data Source=.; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sasa@123;";
        private readonly AdoDotNetService _adoDotNetService;

        public AdoDotNetExample2()
        {
            _adoDotNetService = new AdoDotNetService(_connectionString);
        }

        public void Read()
        {
            string query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthor]
                          ,[BlogContent]
                          ,[DeleteFlag]
                      FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";

            var dt = _adoDotNetService.Query(query);
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(value: dr[columnName: "BlogId"]);
                Console.WriteLine(value: dr[columnName: "BlogTitle"]);
                Console.WriteLine(value: dr[columnName: "BlogAuthor"]);
                Console.WriteLine(value: dr[columnName: "BlogContent"]);
            }
        }

        public void Edit()
        {
            Console.Write("Search Blog ID");
            string id = Console.ReadLine();

            string query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthor]
                          ,[BlogContent]
                          ,[DeleteFlag]
                      FROM [dbo].[Tbl_Blog] where BlogId = @BlogId";

            //SqlParameterModel[] sqlParameters = new SqlParameterModel[1];
            //sqlParameters[0] = new SqlParameterModel
            //{
            //    Name = "@BlogId",
            //    Value = id
            //};
            //var dt = _adoDotNetService.Query(query, sqlParameters);

            var dt = _adoDotNetService.Query(query,
                new SqlParameterModel("@BlogId", id));

            DataRow dr = dt.Rows[0];
            Console.WriteLine(value: dr[columnName: "BlogId"]);
            Console.WriteLine(value: dr[columnName: "BlogTitle"]);
            Console.WriteLine(value: dr[columnName: "BlogAuthor"]);
            Console.WriteLine(value: dr[columnName: "BlogContent"]);
        }

        public void Create()
        {
            // This is ADO.NET Create, Edit, Update

            Console.Write("Enter Blog Title : ");
            string title = Console.ReadLine();

            Console.Write("Enter Blog Author : ");
            string author = Console.ReadLine();

            Console.Write("Enter Blog Content : ");
            string content = Console.ReadLine();

            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
                           ([BlogTitle]
                           ,[BlogAuthor]
                           ,[BlogContent]
                           ,[DeleteFlag])
                     VALUES
                           (@BlogTitle
                           ,@BlogAuthor
                           ,@BlogContent
                           ,@DeleteFlag)";

            int result = _adoDotNetService.Execute(query, 
                new SqlParameterModel("@BlogTitle", title),
                new SqlParameterModel("@BlogAuthor", author),
                new SqlParameterModel("BlogContent", content));

            Console.WriteLine(result == 0 ? "Saving Successfull." : "Saving Failed.");

        }

    }
}
