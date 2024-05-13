using KSTDotNetCore.RestApiWithNLayer.Db;

namespace KSTDotNetCore.RestApiWithNLayer.Features.Blog
{
    // Data Access
    public class DA_Blog
    {
        private readonly AppDbContext _context;

        public DA_Blog()
        {
            _context = new AppDbContext();
        }


        public List<BlogModel> GetBlogs()
        {
            var lst = _context.Blogs.ToList();
            return lst;
        }


        public BlogModel GetBlog(int id)
        {
            var item = _context.Blogs.FirstOrDefault();
            return item;
        }


        public int CreatBlog(BlogModel requestModel)
        { 
            _context.Blogs.Add(requestModel);
            int result = _context.SaveChanges();
            return result;
        }


        public int UpdateBlog(int id, BlogModel requestModel)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                return 0;
            }

            requestModel.BlogTitle = item.BlogTitle;
            requestModel.BlogAuthor = item.BlogAuthor;
            requestModel.BlogContent = item.BlogContent;

            int result = _context.SaveChanges();
            return result;
        }

        public int PatchBlog(int id, BlogModel requestModel)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                return 0;
            }
            if (!string.IsNullOrEmpty(requestModel.BlogTitle))
            {
                item.BlogTitle = requestModel.BlogTitle;
            }
            if (!string.IsNullOrEmpty(requestModel.BlogAuthor))
            {
                item.BlogAuthor = requestModel.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(requestModel.BlogContent))
            {
                item.BlogContent = requestModel.BlogContent;
            }

            int result = _context.SaveChanges();
            return result;
        }


        public int DeleteBlog(int id)
        {
            var item = _context.Blogs.FirstOrDefault(x=> x.BlogId==id);
            _context.Blogs.Remove(item);
            int result = _context.SaveChanges();
            return result;
        }
    }
}
