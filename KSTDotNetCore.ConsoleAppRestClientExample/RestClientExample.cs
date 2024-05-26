using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KSTDotNetCore.ConsoleAppRestClientExample
{
    internal class RestClientExample
    {
        private readonly RestClient _client = new RestClient(new Uri("https://localhost:7053"));
        private readonly string _blogEndPoint = "api/Blog";
        public async Task runAsync()
        {
            await readAsync();
            //await ViewAsync(1);
            //await ViewAsync(100);
            //await DeleteAsync(100);
            //await CreatAsync("Title", "Author", "Content");
            //await UpdateAsync(5, "TITLE", "AUTHOR", "CONTENT");
            //await ViewAsync(5);
        }

        private async Task readAsync()
        {
            //RestRequest request = new RestRequest(_blogEndPoint);
            //var response = await _client.GetAsync(request);

            RestRequest request = new RestRequest(_blogEndPoint, method:Method.Get);
            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr =  response.Content!;
                List<BlogModel> lst = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr)!;

                foreach (var item in lst)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(item));
                    Console.WriteLine($"ID => {item.BlogId}");
                    Console.WriteLine($"Title => {item.BlogTitle}");
                    Console.WriteLine($"Author => {item.BlogAuthor}");
                    Console.WriteLine($"Content => {item.BlogContent}");
                }
            }

        }

        private async Task ViewAsync(int id)
        {
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{id}", method:Method.Get);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                var item = JsonConvert.DeserializeObject<BlogModel>(jsonStr)!;

                Console.WriteLine(JsonConvert.SerializeObject(item));
                Console.WriteLine($"ID => {item.BlogId}");
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}");
            }
            else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }


        private async Task CreatAsync(string title, string author, string content)
        {
            //To C# Object
            BlogModel model = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            RestRequest restRequest = new RestRequest(_blogEndPoint, method:Method.Get);
            restRequest.AddJsonBody(model);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string messaage = response.Content!;
                Console.WriteLine(messaage);
            }
        }
        private async Task UpdateAsync(int id, string title, string author, string content)
        {
            BlogModel model = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{id}", method: Method.Put);
            restRequest.AddJsonBody(model) ;
            var response = await _client.PutAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }


        private async Task DeleteAsync(int id)
        {
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{id}", method: Method.Get);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
            else
            {
                string message = response.Content!;
                Console.WriteLine(message); 
            }
        }
    }
}
