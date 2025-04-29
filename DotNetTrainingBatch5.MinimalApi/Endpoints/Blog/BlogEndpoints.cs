namespace DotNetTrainingBatch5.MinimalApi.Endpoints.Blog
{
    public static class BlogEndpoints
    {
        /*public static string Test(this int i)
        {
            return i.ToString();
        }*/

        public static void MapBlogEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/blogs", () =>
            {
                AppDbContext db = new AppDbContext();
                var model = db.TblBlogs.AsNoTracking().ToList();
                return Results.Ok(model);
            })
            .WithName("GetBlogs")
            .WithOpenApi();

            //Get by Id
            app.MapGet("/blogs/{id}", (int id) =>
            {
                AppDbContext db = new AppDbContext();
                var item = db.TblBlogs
                            .AsNoTracking()
                            .FirstOrDefault(x => x.BlogId == id);
                if (item is not null)
                {
                    return Results.BadRequest("No Data Found.");
                }

                return Results.Ok(item);
            })
            .WithName("GetBlog")
            .WithOpenApi();

            app.MapPost("/blogs", (TblBlog blog) =>
            {
                AppDbContext db = new AppDbContext();
                db.TblBlogs.Add(blog);
                db.SaveChanges();
                return Results.Ok(blog);
            })
            .WithName("CreateBlogs")
            .WithOpenApi();

            app.MapPut("/blogs/{id}", (int id, TblBlog blog) =>
            {
                AppDbContext db = new AppDbContext();
                var item = db.TblBlogs
                            .AsNoTracking()
                            .FirstOrDefault(x => x.BlogId == id);
                if (item is not null)
                {
                    return Results.BadRequest("No Data Found.");
                }

                item.BlogTitle = blog.BlogTitle;
                item.BlogAuthor = blog.BlogAuthor;
                item.BlogContent = blog.BlogContent;

                db.Entry(item).State = EntityState.Modified;

                db.SaveChanges();
                return Results.Ok(blog);
            })
            .WithName("UpdateBlogs")
            .WithOpenApi();

            app.MapDelete("/blogs/{id}", (int id) =>
            {
                AppDbContext db = new AppDbContext();
                var item = db.TblBlogs
                            .AsNoTracking()
                            .FirstOrDefault(x => x.BlogId == id);
                if (item is not null)
                {
                    return Results.BadRequest("No Data Found.");
                }

                db.Entry(item).State = EntityState.Deleted;

                db.SaveChanges();
                return Results.Ok();
            })
            .WithName("DeleteBlogs")
            .WithOpenApi();

        }

    }
}
