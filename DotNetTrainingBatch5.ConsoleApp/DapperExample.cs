using Dapper;
using DotNetTrainingBatch5.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.ConsoleApp
{
    public class DapperExample
    {

        private readonly string _connectionString = "Data Source=.; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sasa@123;";

        public void Read()
        {
            //using(IDbConnection db = new SqlConnection(_connectionString))
            //{
            //    string query = "select * from tbl_blog where DeleteFlag = 0;";
            //    var lst = db.Query(query).ToList();
            //    foreach (var item in lst)
            //    {
            //        Console.WriteLine(item.BlogId);
            //        Console.WriteLine(item.BlogTitle);
            //        Console.WriteLine(item.BlogAuthor);
            //        Console.WriteLine(item.BlogContent);
            //    }
            //}

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from tbl_blog where DeleteFlag = 0;";
                var lst = db.Query<BlogDataModel>(query).ToList();
                foreach (var item in lst)
                {
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);
                }
            }
        }

        public void Create(string title, string author, string content)
        {
            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
                           ([BlogTitle]
                           ,[BlogAuthor]
                           ,[BlogContent]
                           ,[DeleteFlag])
                     VALUES
                           (@BlogTitle
                           ,@BlogAuthor
                           ,@BlogContent
                           ,0)";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });

                Console.WriteLine(result == 1 ? "Saving Successfull." : "Saving Failed.");
            }
        }

        public void Update()
        {
            Console.Write("Enter Blog id :");
            string id = Console.ReadLine();

            Console.Write("Enter Blog Title : ");
            string title = Console.ReadLine();

            Console.Write("Enter Blog Author : ");
            string author = Console.ReadLine();

            Console.Write("Enter Blog Content : ");
            string content = Console.ReadLine();

            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                              ,[DeleteFlag] = 0
                         WHERE BlogId = @BlogId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new
                {
                    BlogId = id,
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });

                Console.WriteLine(result == 1 ? "Updating Successfull." : "Updating Failed.");
            }
        }

        public void Delete()
        {
            Console.Write("Enter Blog id to delete :");
            string id = Console.ReadLine();

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                            WHERE BlogId = @BlogId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new
                { 
                    BlogId = id
                });

                Console.WriteLine(result == 1 ? "Deleting Successfull." : "Deleting Failed.");
            }
        }
    }
}
