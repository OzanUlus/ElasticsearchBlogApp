using Microsoft.AspNetCore.Mvc;

namespace ElasticsearchBlogApp.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Save()
        {
            return View();
        }
    }
}
