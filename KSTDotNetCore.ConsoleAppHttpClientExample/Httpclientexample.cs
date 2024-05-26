using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KSTDotNetCore.ConsoleAppHttpClientExample
{
    internal class Httpclientexample
    {
        private readonly HttpClient _client = new HttpClient() { BaseAddress = new Uri("https://localhost:7053") };
        private readonly string _blogEndPoint = "api/Blog";
        public async Task runAsync()
        {
            //await readAsync();
            //await ViewAsync(1);
            //await ViewAsync(100);
            //await DeleteAsync(100);
            //await CreatAsync("Title", "Author", "Content");
            await UpdateAsync(5, "TITLE", "AUTHOR", "CONTENT");
            await ViewAsync(5);
        }

        private async Task readAsync()
        {
            var response = await _client.GetAsync(_blogEndPoint);
            if(response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                List<BlogModel> lst = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr) !;

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
            var response = await _client.GetAsync($"{_blogEndPoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<BlogModel>(jsonStr)!;

                Console.WriteLine(JsonConvert.SerializeObject(item));
                Console.WriteLine($"ID => {item.BlogId}");
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}");
            } 
            else
            {
                string message = await response.Content.ReadAsStringAsync();
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
            
            //To Json
            string blogStr = JsonConvert.SerializeObject(model);
            HttpContent httpContent = new StringContent(blogStr, Encoding.UTF8, Application.Json);

            var response = await _client.PostAsync(_blogEndPoint, httpContent);
            if (response.IsSuccessStatusCode)
            {
                string messaage = await response.Content.ReadAsStringAsync();
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
            string blogStr = JsonConvert.SerializeObject(model);
            HttpContent httpContent= new StringContent(blogStr, Encoding.UTF8 , Application.Json);

            var response = await _client.PutAsync($"{_blogEndPoint}/{id}", httpContent);
            if(response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        private async Task DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"{_blogEndPoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }
    }
}
