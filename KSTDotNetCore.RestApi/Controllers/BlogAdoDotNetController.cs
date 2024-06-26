﻿using KSTDotNetCore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace KSTDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            SqlConnection conn = new SqlConnection(Connectionstrings.sqlConnectionStringBuilder.ConnectionString);
            conn.Open();

            string query = "select * from Tbl_Blog";
            SqlCommand cmd = new SqlCommand(query, conn);            
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            //List<BlogModel> list = new List<BlogModel>();
            //foreach (DataRow dr in dt.Rows)
            //{
                //BlogModel blog = new BlogModel();
                //blog.BlogId = Convert.ToInt32(dr["BlogId"]);
                //blog.BlogTitle = Convert.ToString(dr["BlogTitle"]);
                //blog.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
                //blog.BlogContent = Convert.ToString(dr["BlogContent"]);

                //BlogModel blog = new BlogModel()
                //{
                //    BlogId = Convert.ToInt32(dr["BlogId"]),
                //    BlogTitle = Convert.ToString(dr["BlogTitle"]),
                //    BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                //    BlogContent = Convert.ToString(dr["BlogContent"])
                //};
                //list.Add(blog);
            //}

            List <BlogModel> list = dt.AsEnumerable().Select(dr => new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            }).ToList();

            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "select * from Tbl_Blog where BlogId = @BlogId";
            SqlConnection conn = new SqlConnection(Connectionstrings.sqlConnectionStringBuilder.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            conn.Close();

            if (dt.Rows.Count == 0)
            {
                return NotFound("No data found");
            }
            DataRow dr = dt.Rows[0];
            var item = new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            };

            return Ok(item);            
        }


        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            SqlConnection conn = new SqlConnection (Connectionstrings.sqlConnectionStringBuilder.ConnectionString);
            conn.Open();
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

            SqlCommand cmd  = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();
            conn.Close();

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

            SqlConnection conn = new SqlConnection(Connectionstrings.sqlConnectionStringBuilder.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            cmd.Parameters.AddWithValue("@BlogId", id);

            int result = cmd.ExecuteNonQuery ();
            conn.Close();

            string message = result > 0 ? "Updating  Successful" : "Saving failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog (int id, BlogModel blog)
        {
            string condition = string.Empty;
            if(!string.IsNullOrEmpty(blog.BlogTitle))
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

            if (condition.Length == 0)
            {
                return Ok("No data to update");
            }

            condition = condition.Substring(0, condition.Length - 2);

            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {condition}
 WHERE BlogId = @BlogId";

            SqlConnection conn = new SqlConnection(Connectionstrings.sqlConnectionStringBuilder.ConnectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            }
            cmd.Parameters.AddWithValue("@BlogId", id);

            int result = cmd.ExecuteNonQuery();
            conn.Close();

            string message = result > 0 ? "Updating  Successful" : "Saving failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId = @BlogId";

            SqlConnection conn = new SqlConnection(Connectionstrings.sqlConnectionStringBuilder.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();
            conn.Close();

            string message = result > 0 ? "Deleting  Successful" : "Deleting failed";
            return Ok(message);

        }
    }
}
