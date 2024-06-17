using Dapper;
using KSTDotNetCore.RestApi.Models;
using KSTDotNetCore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace KSTDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapper2Controller : ControllerBase
    {
        //private readonly DapperService _dapperService = new DapperService(Connectionstrings.sqlConnectionStringBuilder.ConnectionString);

        private readonly DapperService _dapperService;

        public BlogDapper2Controller(DapperService dapperService)
        {
            _dapperService = dapperService;
        }


        //-----Read-----
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from Tbl_Blog";
            var lst =_dapperService.Query<BlogModel>(query);
            return Ok(lst);
        }

        //-------view------
        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = findById(id);
            if (item is null)
            {
                return NotFound("Data not found");
            }
            return Ok(item);
        }

        //--------Create-------
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

            int result = _dapperService.Execute(query,blog);
            string message = result > 0 ? "Creating Successful" : "Creating Failed";
            return Ok(message);
        }

        //------------Update----------------
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            var item = findById(id);
            if (item is null)
            {
                return NotFound("No data found.");
            }

            blog.BlogId = id;
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query, blog);
            string message = result > 0 ? "Updating successful" : "Updating Failed";
            return Ok(message);
        }

        //--------------Patch----------------
        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            var item = findById(id);
            if (item is null)
            {
                return NotFound("Data not found");
            }

            string condition = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                condition += "[BlogTitle] = @BlogTitle, ";
            }

            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                condition += "[BlogAuthor] = @BlogAuthor, ";
            }

            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                condition += "[BlogContent] = @BlogContent, ";
            }

            if(condition.Length == 0)
            {
                return NotFound("No data to update");
            }

            condition = condition.Substring(0, condition.Length - 2);
            blog.BlogId = id;

            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {condition}
 WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query, blog);
            string message = result > 0 ? "Patching Successful" : "Patching Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = findById(id);
            if(item is null)
            {
                return NotFound("No data found");
            }

            string query = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(Connectionstrings.sqlConnectionStringBuilder.ConnectionString);

            int result = _dapperService.Execute(query, new BlogModel { BlogId = id });
            string message = result > 0 ? "Deleting successful" : "Deleting Failed";
            return Ok(message);
        }

        private BlogModel findById(int id)
        {
            string query = "select * from Tbl_BLog where BlogId = @BlogId";
            var item = _dapperService.QueryFirstOrDefault<BlogModel>(query, new BlogModel { BlogId = id});
            return item;
        }
    }
}
