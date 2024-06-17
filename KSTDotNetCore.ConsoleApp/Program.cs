// See https://aka.ms/new-console-template for more information
using KSTDotNetCore.ConsoleApp.AdoDotNetExamples;
using KSTDotNetCore.ConsoleApp.DapperExamples;
using KSTDotNetCore.ConsoleApp.EFCoreExamples;
using KSTDotNetCore.ConsoleApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;


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
////}

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
////adoDotNetExample.Read();
//adoDotNetExample.Create("title", "author", "content");
//adoDotNetExample.Update(12, "test title", "test author", "test content");
//adoDotNetExample.Delete(13);
//adoDotNetExample.Edit(14);  


//DapperExample dapperExample = new DapperExample();
//dapperExample.Run();


//EFCoreExample ef = new EFCoreExample();
//ef.Run();


//try
//{

//}
//catch (Exception ex)
//{

//	throw;
//}
//finally
//{

//}
var connectionString = Connectionstrings.sqlConnectionStringBuilder.ConnectionString;
var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

var serviceProvider = new ServiceCollection()
    .AddScoped(n=>new AdoDotNetExample(sqlConnectionStringBuilder))
    .AddScoped(n => new DapperExample(sqlConnectionStringBuilder))
    .AddDbContext<AppDbContext>(opt =>
    {
        opt.UseSqlServer(connectionString);
    })
    .AddScoped<EFCoreExample>()
    .BuildServiceProvider();

//AppDbContext db = serviceProvider.GetRequiredService<AppDbContext>();

var adoDotNetExample = serviceProvider.GetRequiredService<AdoDotNetExample>();
adoDotNetExample.Read();

//var dapperExample = serviceProvider.GetRequiredService<DapperExample>();
//dapperExample.Run();

Console.ReadKey(); 