// See https://aka.ms/new-console-template for more information
using KSTDotNetCore.ConsoleApp;
using System.Data;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

Console.WriteLine("Hello, World!");


//SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
//stringBuilder.DataSource = ".";
//stringBuilder.InitialCatalog = "DotNetTrainningBatch4";
//stringBuilder.UserID = "sa";
//stringBuilder.Password = "sa@123";
//SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
////SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=DotNetTrainningBatch4;User ID=sa;Password=sa@123");


//connection.Open();
//Console.WriteLine("Connection Open.");

//string query = "select * from Tbl_Blog";
//SqlCommand cmd = new SqlCommand(query, connection);
//SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
//DataTable dt = new DataTable();
//sqlDataAdapter.Fill(dt);

//connection.Close();
//Console.WriteLine("Connection Close.");

//foreach (DataRow dr in dt.Rows)
//{
//    Console.WriteLine("Blog Id =>"+dr["BlogId"]);
//    Console.WriteLine("Blog Title =>" + dr["BlogTitle"]);
//    Console.WriteLine("Blog Author =>" + dr["BlogAuthor"]);
//    Console.WriteLine("Blog Content =>" + dr["BlogContent"]);
//    Console.WriteLine("...........................................");
//}

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create("title", "author", "content");
//adoDotNetExample.Update(12, "test title", "test author", "test content");
//adoDotNetExample.Delete(13);
adoDotNetExample.Edit(14);  




Console.ReadKey();