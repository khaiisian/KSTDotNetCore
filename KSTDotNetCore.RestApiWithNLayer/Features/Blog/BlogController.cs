using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KSTDotNetCore.RestApiWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BL_Blog _bl_Blog;

        public BlogController()
        {
            _bl_Blog = new BL_Blog();
        }


        [HttpGet]
        public IActionResult Read()
        {
            var lst = _bl_Blog.GetBlogs();
            return Ok(lst);
        }


        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _bl_Blog.GetBlog(id);
            return Ok(item);
        }


        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            int result = _bl_Blog.CreateBlog(blog);

            string message = result > 0 ? "Creating successful" : "Creating Failed";
            return Ok(message);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = _bl_Blog.GetBlog(id);
            if (item is null){
                return NotFound("No data found to update");
            }

            int result = _bl_Blog.UpdateBlog(id, blog);
            string message = result > 0 ? "Updating successful" : "Updating Failed";
            return Ok(message); 
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = _bl_Blog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No data found to patch");
            }

            int result = _bl_Blog.PatchBlog(id, blog);
            string message = result > 0 ? "Patching Successful" : "Patchin Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _bl_Blog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No data found to delete");
            }

            int result = _bl_Blog.DeleteBlog(id);
            string message = result > 0 ? "Deleting successful" : "Deleting failed"; 
            return Ok(message);
        }
    }
}
