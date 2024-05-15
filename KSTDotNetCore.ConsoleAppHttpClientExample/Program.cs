// See https://aka.ms/new-console-template for more information
using KSTDotNetCore.ConsoleAppHttpClientExample;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

//HttpClient client = new HttpClient();
//var response = await client.GetAsync("https://localhost:7053/api/Blog");
//if (response.IsSuccessStatusCode)
//{
//    string jsonStr = await response.Content.ReadAsStringAsync();
//    //Console.WriteLine(jsonStr); 
//    List <BlogModel> lst = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr)!;

//    foreach (var blog in lst)
//    {
//        Console.WriteLine(JsonConvert.SerializeObject(blog));
//        Console.WriteLine($"Id=> {blog.BlogId}");
//        Console.WriteLine($"Title=> {blog.BlogTitle}");
//        Console.WriteLine($"Author=> {blog.BlogAuthor}");
//        Console.WriteLine($"Content=> {blog.BlogContent}");
//    }
//

Httpclientexample example = new Httpclientexample();
await example.runAsync();
Console.ReadLine();