using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace KSTDotNetCore.ConsoleAppRefitExamples
{
    public class RefitExamples
    {
        private readonly IBlogApi _service = RestService.For<IBlogApi>("https://localhost:7106");
        public async Task RunAsync()
        {
            //await ReadAsync();
            //await EditAsync(5);
            //await EditAsync(100);
            //await CreateAsync("Title", "Author", "Content");
            //await UpdateAsync(100, "title", "author", "content");
            await DeleteAsync(105);
        }

        private async Task ReadAsync()
        {
            var lst = await _service.GetBlogs();
            foreach (var item in lst)
            {
                Console.WriteLine($"Id => {item.BlogId}");
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}");
                Console.WriteLine("---------------------------------");
            }
        }

        private async Task EditAsync(int id)
        {
            //Exception=> //Refit.ApiException: 'An error occured deserializing the response.'
            try
            {
                var item = await _service.GetBlog(id);
                Console.WriteLine($"Id => {item.BlogId}");
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}");
                Console.WriteLine("---------------------------------");
            }
            catch(ApiException ex)
            {
                Console.WriteLine(ex.StatusCode.ToString());
                Console.WriteLine(ex.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task CreateAsync (string title,  string author, string content)
        {
            BlogModel blog = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            var message = await _service.CreateBlog(blog);
            Console.WriteLine(message);
        }

        private async Task UpdateAsync(int id, string title, string author, string content)
        {
            BlogModel blog = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            try
            {
                var message = await _service.UpdateBlog(id, blog);
                Console.WriteLine(message);
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.StatusCode.ToString());
                Console.WriteLine(ex.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task DeleteAsync(int id)
        {
            try
            {
                var message = await _service.DeleteBlog(id);
                Console.WriteLine(message);
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.StatusCode.ToString());
                Console.WriteLine(ex.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
