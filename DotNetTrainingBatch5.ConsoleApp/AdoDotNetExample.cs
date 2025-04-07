using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.ConsoleApp
{
    public class AdoDotNetExample
    {

        private readonly string _connectionString = "Data Source=.; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sasa@123;";

        public void Read()
        {
            Console.WriteLine("Hello, World!");
            Console.ReadLine();
            Console.WriteLine(value: "Connection String : " + _connectionString);
            SqlConnection connection = new SqlConnection(_connectionString);

            Console.WriteLine(value: "Connection Openning...");
            connection.Open();
            Console.WriteLine(value: "Connection Opended.");

            string query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthor]
                          ,[BlogContent]
                          ,[DeleteFlag]
                      FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";

            SqlCommand cmd = new SqlCommand(cmdText: query, connection);
            //SqlDataAdapter adapter = new SqlDataAdapter(selectCommand: cmd);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(value: reader["BlogId"]);
                Console.WriteLine(value: reader["BlogTitle"]);
                Console.WriteLine(value: reader["BlogAuthor"]);
                Console.WriteLine(value: reader["BlogContent"]);
            }

            Console.WriteLine(value: "Connection Closing...");
            connection.Close();
            Console.WriteLine(value: "Connection Closed.");

            //foreach (DataRow dr in dt.Rows)
            //{
            //    Console.WriteLine(value: dr[columnName: "BlogId"]);
            //    Console.WriteLine(value: dr[columnName: "BlogTitle"]);
            //    Console.WriteLine(value: dr[columnName: "BlogAuthor"]);
            //    Console.WriteLine(value: dr[columnName: "BlogContent"]);
            //}
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

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            //string query1 = $@"INSERT INTO [dbo].[Tbl_Blog]
            //           ([BlogTitle]
            //           ,[BlogAuthor]
            //           ,[BlogContent]
            //           ,[DeleteFlag])
            //     VALUES
            //           ('{title}'
            //           ,'{author}'
            //           ,'{content}'
            //           ,0)";

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

            SqlCommand cmd = new SqlCommand(@query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogTitle", author);
            cmd.Parameters.AddWithValue("@BlogTitle", content);

            //SqlDataAdapter adapter = new SqlDataAdapter(cmd1);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            //if (result == 0)
            //{
            //    Console.WriteLine("Saving Successfull.");
            //}
            //else
            //{
            //    Console.WriteLine("Saving Failed.");
            //}

            Console.WriteLine(result == 0 ? "Saving Successfull." : "Saving Failed.");

        }
    
        public void Edit()
        {
            Console.Write("Search Blog ID");
            string id = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthor]
                          ,[BlogContent]
                          ,[DeleteFlag]
                      FROM [dbo].[Tbl_Blog] where BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(@query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand: cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if(dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found.");
                return;
            }
            DataRow dr = dt.Rows[0];
            Console.WriteLine(value: dr[columnName: "BlogId"]);
            Console.WriteLine(value: dr[columnName: "BlogTitle"]);
            Console.WriteLine(value: dr[columnName: "BlogAuthor"]);
            Console.WriteLine(value: dr[columnName: "BlogContent"]);

        }

        public void Update()
        {
            Console.Write("Enter Blog id :");
            string id = Console.ReadLine();

            Console.Write("Enter Blog Title :");
            string title = Console.ReadLine();

            Console.Write("Enter Blog Author :");
            string author = Console.ReadLine();

            Console.Write("Enter Blog Content :");
            string content = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                              ,[DeleteFlag] = 0
                         WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(@query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine(result == 1 ? "Updating Successfull." : "Saving Failed.");
        }
    
        public void Delete()
        {
            Console.Write("Enter Blog id to delete :");
            string id = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                            WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(@query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine(result == 0 ? "Deleting Failed." : "Delete Successfull.");
        }
    
    }

}
