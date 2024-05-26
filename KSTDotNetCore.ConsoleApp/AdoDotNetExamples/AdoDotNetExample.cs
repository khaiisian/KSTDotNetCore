using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Metadata;

namespace KSTDotNetCore.ConsoleApp.AdoDotNetExamples
{
    internal class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "DotNetTrainingBatch4",
            UserID = "sa",
            Password = "sa@123"
        };
        public void Read()
        {
            //SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
            //stringBuilder.DataSource = ".";
            //stringBuilder.InitialCatalog = "DotNetTrainningBatch4";
            //stringBuilder.UserID = "sa";
            //stringBuilder.Password = "sa@123";

            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            //SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=DotNetTrainningBatch4;User ID=sa;Password=sa@123");


            connection.Open();
            Console.WriteLine("Connection Open.");

            string query = "select * from Tbl_Blog";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();
            Console.WriteLine("Connection Close.");

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("Blog Id =>" + dr["BlogId"]+"'");
                Console.WriteLine("Blog Title =>" + dr["BlogTitle"]+"'");
                Console.WriteLine("Blog Author =>" + dr["BlogAuthor"] + "'");
                Console.WriteLine("Blog Content =>" + dr["BlogContent"] + "'");
                Console.WriteLine("...........................................");
            }
        }

        //-------------Edit-------------------------------------------------------------------------

        public void Edit(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            //SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=DotNetTrainningBatch4;User ID=sa;Password=sa@123");


            connection.Open();
            Console.WriteLine("Connection Open.");

            string query = "select * from Tbl_Blog where BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();
            Console.WriteLine("Connection Close.");

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found");
                return;
            }

            DataRow dr = dt.Rows[0];

            Console.WriteLine("Blog Id =>" + dr["BlogId"]);
            Console.WriteLine("Blog Title =>" + dr["BlogTitle"]);
            Console.WriteLine("Blog Author =>" + dr["BlogAuthor"]);
            Console.WriteLine("Blog Content =>" + dr["BlogContent"]);
            Console.WriteLine("...........................................");

        }

        public void Create(string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            //int result = cmd.ExecuteNonQuery();

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Saving Successful" : "Saving failed";
            Console.WriteLine(message);
        }

        public void Update(int id, string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            //int result = cmd.ExecuteNonQuery();

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Updating  Successful" : "Saving failed";
            Console.WriteLine(message);
        }

        //------------DELETE-----------------------------------------------------------------------------------
        //  public void Delete (int id)
        //  {
        //      SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
        //      connection.Open();

        //      string query = @"DELETE FROM [dbo].[Tbl_Blog]
        //WHERE BlogId = @BlogId";

        //      SqlCommand cmd = new SqlCommand(query, connection);
        //      cmd.Parameters.AddWithValue("@BlogId", id);

        //      int result = cmd.ExecuteNonQuery();

        //      connection.Close();

        //      string message = result > 0 ? "Deleting  Successful" : "Saving failed";
        //      Console.WriteLine(message);
        //  }

        public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);

            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Deleting successful" : "Deleing failed";
            Console.WriteLine(message);
        }
    }
}
