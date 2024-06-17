using KSTDotNetCore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using KSTDotNetCore.Shared;
using System.Security.Cryptography;

namespace KSTDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNet2Controller : ControllerBase
    {
        //private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(Connectionstrings.sqlConnectionStringBuilder.ConnectionString);

        private readonly AdoDotNetService _adoDotNetService;

        public BlogAdoDotNet2Controller(AdoDotNetService adoDotNetService)
        {
            _adoDotNetService = adoDotNetService;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from Tbl_Blog";
            var lst = _adoDotNetService.Query<BlogModel>(query);

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "select * from Tbl_Blog where BlogId = @BlogId";
            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query,
                new AdoDotNetParameter("@BlogId", id)
                );

            if (item is null )
            {
                return NotFound("No data found");
            }
           
            return Ok(item);            
        }


        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent)
            );

            string message = result > 0 ? "Creating successful" : "Creating Failed";
            //return Ok(message);

            return StatusCode (500, message);
        }


        [HttpPut ("{id}")]
        public IActionResult UpdateBlog (int id, BlogModel blog)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";

            
            int result = _adoDotNetService.Execute1(query,
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent),
                new AdoDotNetParameter("@BlogId", id)

            );

            string message = result > 0 ? "Updating  Successful" : "Saving failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog (int id, BlogModel blog)
        {
            string condition = string.Empty;
            AdoDotNetParameter[] parameters = new AdoDotNetParameter[4];
            parameters[0] = new AdoDotNetParameter("@BlogId", id);

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                condition += "[BlogTitle] = @BlogTitle, ";
                parameters[1] = new AdoDotNetParameter("@BlogTitle", blog.BlogTitle);
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                condition += "[BlogAuthor] = @BlogAuthor, ";
                parameters[2] = new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor);
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                condition += "[BlogContent] = @BlogContent, ";
                parameters[3] = new AdoDotNetParameter("@BlogContent", blog.BlogContent);
            }

            if (condition.Length == 0)
            {
                return Ok("No data to update");
            }

            new AdoDotNetParameter("@BlogId", id);

            condition = condition.Substring(0, condition.Length - 2);

            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {condition}
 WHERE BlogId = @BlogId";
            int result = _adoDotNetService.Execute1(query, parameters);
            string message = result > 0 ? "Updating  Successful" : "Saving failed";
            return Ok(message);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId = @BlogId";

            int result = _adoDotNetService.Execute1(query,
                new AdoDotNetParameter("@BlogId", id));
            string message = result > 0 ? "Deleting  Successful" : "Deleting failed";
            return Ok(message);
        }
    }
}
