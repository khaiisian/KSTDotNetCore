using KSTDotNetCore.ConsoleApp.EFCoreExamples;
using KSTDotNetCore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace KSTDotNetCore.RestApi.Controllers
{
    //https://localhost:3000 => Domain Url
    //api/blog => end point
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public BlogController()
        {
            _appDbContext = new AppDbContext();
        }

        // Read
        [HttpGet]
        public IActionResult Read()
        {
            var lst = _appDbContext.Blogs.ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No data found");
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            _appDbContext.Blogs.Add(blog);
            var result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Saving successful" : "Saving Failed";
            return Ok(message);
        }

        // change the whole resource
        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("Not found");
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            var result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Saving successful" : "Saving failed";

            return Ok(message);
        }

        // partial update
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                return NotFound("Not found");
            }

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }

            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }

            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }

            int result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Updating successful" : "Updating Failed";

            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                return NotFound("Not found");
            }

            _appDbContext.Remove(item);
            int result = _appDbContext.SaveChanges();

            string message = result > 0 ? "Deleting successful" : "Deleting failed";
            return Ok(message);
        }
    }
}
