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
        private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(Connectionstrings.sqlConnectionStringBuilder.ConnectionString);

        [HttpGet]
        public IActionResult GetBlogs()
        {
            //SqlConnection conn = new SqlConnection(Connectionstrings.sqlConnectionStringBuilder.ConnectionString);
            //conn.Open();

            string query = "select * from Tbl_Blog";
            //SqlCommand cmd = new SqlCommand(query, conn);            
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);

            ////List<BlogModel> list = new List<BlogModel>();
            ////foreach (DataRow dr in dt.Rows)
            ////{
            //    //BlogModel blog = new BlogModel();
            //    //blog.BlogId = Convert.ToInt32(dr["BlogId"]);
            //    //blog.BlogTitle = Convert.ToString(dr["BlogTitle"]);
            //    //blog.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
            //    //blog.BlogContent = Convert.ToString(dr["BlogContent"]);

            //    //BlogModel blog = new BlogModel()
            //    //{
            //    //    BlogId = Convert.ToInt32(dr["BlogId"]),
            //    //    BlogTitle = Convert.ToString(dr["BlogTitle"]),
            //    //    BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            //    //    BlogContent = Convert.ToString(dr["BlogContent"])
            //    //};
            //    //list.Add(blog);
            ////}

            //List <BlogModel> list = dt.AsEnumerable().Select(dr => new BlogModel
            //{
            //    BlogId = Convert.ToInt32(dr["BlogId"]),
            //    BlogTitle = Convert.ToString(dr["BlogTitle"]),
            //    BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            //    BlogContent = Convert.ToString(dr["BlogContent"])
            //}).ToList();

            var lst = _adoDotNetService.Query<BlogModel>(query);

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "select * from Tbl_Blog where BlogId = @BlogId";

            //AdoDotNetParameter[] parameters = new AdoDotNetParameter[1];
            //parameters[0] = new AdoDotNetParameter("@BlogId", id);
            //var lst = _adoDotNetService.Query<BlogModel>(query, parameters);


            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query,
                new AdoDotNetParameter("@BlogId", id)
                );


            //SqlConnection conn = new SqlConnection(Connectionstrings.sqlConnectionStringBuilder.ConnectionString);
            //conn.Open();
            //SqlCommand cmd = new SqlCommand(query, conn);
            //cmd.Parameters.AddWithValue("@BlogId", id);
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);
            //conn.Close();

            if (item is null )
            {
                return NotFound("No data found");
            }
            //DataRow dr = dt.Rows[0];
            //var item = new BlogModel
            //{
            //    BlogId = Convert.ToInt32(dr["BlogId"]),
            //    BlogTitle = Convert.ToString(dr["BlogTitle"]),
            //    BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            //    BlogContent = Convert.ToString(dr["BlogContent"])
            //};

            return Ok(item);            
        }


        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            //SqlConnection conn = new SqlConnection (Connectionstrings.sqlConnectionStringBuilder.ConnectionString);
            //conn.Open();
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

            //SqlCommand cmd  = new SqlCommand(query, conn);
            //cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            //cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            //cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            //int result = cmd.ExecuteNonQuery();
            //conn.Close();

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

            //SqlConnection conn = new SqlConnection(Connectionstrings.sqlConnectionStringBuilder.ConnectionString);
            //conn.Open();
            //SqlCommand cmd = new SqlCommand(query, conn);
            //cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            //cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            //cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            //cmd.Parameters.AddWithValue("@BlogId", id);

            int result = _adoDotNetService.Execute1(query,
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent),
                new AdoDotNetParameter("@BlogId", id)

            );

            //int result = cmd.ExecuteNonQuery ();
            //conn.Close();

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

            //SqlConnection conn = new SqlConnection(Connectionstrings.sqlConnectionStringBuilder.ConnectionString);

            //conn.Open();
            //SqlCommand cmd = new SqlCommand(query, conn);

            //if (!string.IsNullOrEmpty(blog.BlogTitle))
            //{
            //    cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            //}
            //if (!string.IsNullOrEmpty(blog.BlogAuthor))
            //{
            //    cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            //}
            //if (!string.IsNullOrEmpty(blog.BlogContent))
            //{
            //    cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            //}
            //cmd.Parameters.AddWithValue("@BlogId", id);

            //int result = cmd.ExecuteNonQuery();
            //conn.Close();

            //AdoDotNetParameter[] parameters = new AdoDotNetParameter[4];
            //parameters[0] = new AdoDotNetParameter("@BlogId", id);

            //if (!string.IsNullOrEmpty(blog.BlogTitle))
            //{
            //    parameters[1] = new AdoDotNetParameter("@BlogTitle", blog.BlogTitle);
            //}
            //if (!string.IsNullOrEmpty(blog.BlogAuthor))
            //{
            //    parameters[2] = new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor);
            //}
            //if (!string.IsNullOrEmpty(blog.BlogContent))
            //{
            //    parameters[3] = new AdoDotNetParameter("@BlogContent", blog.BlogContent);
            //}
            int result = _adoDotNetService.Execute1(query, parameters);

            string message = result > 0 ? "Updating  Successful" : "Saving failed";
            return Ok(message);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId = @BlogId";

            //SqlConnection conn = new SqlConnection(Connectionstrings.sqlConnectionStringBuilder.ConnectionString);
            //conn.Open();
            //SqlCommand cmd = new SqlCommand(query, conn);
            //cmd.Parameters.AddWithValue("@BlogId", id);
            //int result = cmd.ExecuteNonQuery();
            //conn.Close();

            int result = _adoDotNetService.Execute1(query,
                new AdoDotNetParameter("@BlogId", id));

            string message = result > 0 ? "Deleting  Successful" : "Deleting failed";
            return Ok(message);

        }
    }
}
