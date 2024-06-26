﻿using ElasticsearchBlogApp.Models;
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
              UserId = Guid.NewGuid(),
              Tags = model.Tags.Split(","),
              
          };
            var isCreatedBlog = await _blogRepository.SaveAsync(newBlog);
            return isCreatedBlog != null;
        
        }
        public async Task<List<Blog>> SearchAsync(string searchText) 
        {
         return await _blogRepository.SearchAsync(searchText);
        }
    }
}
