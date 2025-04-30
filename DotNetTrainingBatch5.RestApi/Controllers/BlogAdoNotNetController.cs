using DotNetTrainingBatch5.RestApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DotNetTrainingBatch5.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoNotNetController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sasa@123;TrustServerCertificate=True;";
 
        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModel> lst = new List<BlogViewModel>();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthor]
                          ,[BlogContent]
                          ,[DeleteFlag]
                      FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";

            SqlCommand cmd = new SqlCommand(query, connection);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(value: reader["BlogId"]);
                Console.WriteLine(value: reader["BlogTitle"]);
                Console.WriteLine(value: reader["BlogAuthor"]);
                Console.WriteLine(value: reader["BlogContent"]);

                var item = new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"]),
                };
                lst.Add(item);
            }
            
            connection.Close();
            
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            BlogViewModel? blog = null;

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT [BlogId], [BlogTitle], [BlogAuthor], [BlogContent], [DeleteFlag]
                     FROM [dbo].[Tbl_Blog]
                     WHERE BlogId = @BlogId AND DeleteFlag = 0";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                blog = new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])
                };
            }

            if (blog == null)
                return NotFound($"Blog with ID {id} not found.");

            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlogs(BlogViewModel blog)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

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

            SqlCommand cmd = new SqlCommand(@query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            cmd.Parameters.AddWithValue("@BlogContent", blog.Content);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            return Ok(result == 0 ? "Saving Successfull." : "Saving Failed.");
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, BlogViewModel blog)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                              ,[DeleteFlag] = 0
                         WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            cmd.Parameters.AddWithValue("@BlogContent", blog.Content);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            return Ok(result == 1 ? "Updating Successfull." : "Updating Failed.");
        }
        
        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogViewModel blog)
        {
            string condition = "";
            if (!string.IsNullOrEmpty(blog.Title))
            {
                condition += " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                condition += " [BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                condition += " [BlogContent] = @BlogContent, ";
            }

            if (condition.Length == 0)
            {
                return BadRequest("Invalid Parameter");
            }

            condition = condition.Substring(0, condition.Length - 2);

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = $@"UPDATE [dbo].[Tbl_Blog]
                           SET {condition}
                         WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(@query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            if (!string.IsNullOrEmpty(blog.Title))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                cmd.Parameters.AddWithValue("@BlogContent", blog.Content);
            }

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            return Ok(result > 0 ? "Updating Successful." : "Updating Failed.");
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                            WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            return Ok(result == 0 ? "Deleting Failed." : "Delete Successfull.");
        }
    }
}

