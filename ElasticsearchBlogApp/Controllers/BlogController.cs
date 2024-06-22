using ElasticsearchBlogApp.Models;
using ElasticsearchBlogApp.Services;
using ElasticsearchBlogApp.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ElasticsearchBlogApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }
        public async Task<IActionResult> Search() 
        {
          return View(new List<Blog>());
        }
        [HttpPost]
        public async Task<IActionResult> Search(string searchText)
        {
           var blogList = await _blogService.SearchAsync(searchText);
            return View(blogList);
        }

        public IActionResult Save()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(BlogCreateViewModel model)
        {
            var isSuccess = await _blogService.SaveAsync(model);

            if (!isSuccess) 
            {
                TempData["result"] = "Saving is not success";
                return RedirectToAction(nameof(BlogController.Save));
            }

            TempData["result"] = "Saving is  success";
            return RedirectToAction(nameof(BlogController.Save));
        }
    }
}
