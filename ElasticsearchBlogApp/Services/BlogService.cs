using ElasticsearchBlogApp.Models;
using ElasticsearchBlogApp.Repository;
using ElasticsearchBlogApp.ViewModel;

namespace ElasticsearchBlogApp.Services
{
    public class BlogService
    {
        private readonly BlogRepository _blogRepository;

        public BlogService(BlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<bool> SaveAsync(BlogCreateViewModel model) 
        {
          var newBlog = new Blog 
          {
              Title = model.Title,
              Content = model.Content,
              UserId = new Guid(),
              Tags = model.Tags.ToArray(),
              
          };
            var isCreatedBlog = await _blogRepository.SaveAsync(newBlog);
            return isCreatedBlog != null;
        
        }
    }
}
