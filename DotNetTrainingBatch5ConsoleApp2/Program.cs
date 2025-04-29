// See https://aka.ms/new-console-template for more information
using DotNetTrainingBatch5.Database.Models;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

/*AppDbContext db = new AppDbContext();
var lst = db.TblBlogs.ToList();*/

var blog = new BlogModel
{
    Id = 1,
    Titel = "Test Titel",
    Author = "Test Author",
    Content = "Test Content"
};

string jsonStr = blog.ToJson();

Console.WriteLine(jsonStr);

string jsonStr2 = """{"Id":1,"Titel":"Test Titel","Author":"Test Author","Content":"Test Content"}""";
var blog2 = JsonConvert.DeserializeObject<BlogModel>(jsonStr2);
Console.WriteLine(blog2.Titel);

Console.ReadLine();

public class BlogModel
{
    public int Id { get; set; }
    public string Titel { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }
}

public static class Extension
{
    public static string ToJson(this object obj)
    {
        string jsonStr = JsonConvert.SerializeObject(obj, Formatting.Indented);
        return jsonStr;
    }
}