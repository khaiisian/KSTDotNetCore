using KSTDotNetCore.MinimalApi.Db;
using KSTDotNetCore.MinimalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KSTDotNetCore.MinimalApi.Features.Blog
{
    public static class BlogService
    {
        public static IEndpointRouteBuilder MapBlogs(this IEndpointRouteBuilder app)
        {
            app.MapGet("/", () => "Hello World");

            app.MapGet("api/Blog", async (AppDbContext db) =>
            {
                var lst = await db.Blogs.AsNoTracking().ToListAsync();
                return Results.Ok(lst);
            });

            app.MapPost("api/Blog", async (AppDbContext db, BlogModel blog) =>
            {
                await db.Blogs.AddAsync(blog);
                int result = await db.SaveChangesAsync();

                string message = result > 0 ? "Saving successful" : "Saving Failed";
                return Results.Ok(message);
            });

            app.MapPut("api/Blog", async (AppDbContext db, int id, BlogModel blog) =>
            {
                var item = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.NotFound("No data found");
                }

                item.BlogTitle = blog.BlogTitle;
                item.BlogAuthor = blog.BlogAuthor;
                item.BlogContent = blog.BlogContent;
                int result = db.SaveChanges();

                string message = result > 0 ? "Updating successful" : "Updating Failed";
                return Results.Ok(message);
            });

            app.MapDelete("api/Blog", async (AppDbContext db, int id) =>
            {
                var item = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.NotFound("No data found");
                }
                db.Remove(item);
                int result = await db.SaveChangesAsync();
                string message = result > 0 ? "Deleting successful" : "Deleting Failed";
                return Results.Ok(message);
            });
            return app;
        }
    }
}
