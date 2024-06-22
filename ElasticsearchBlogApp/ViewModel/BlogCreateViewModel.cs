using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ElasticsearchBlogApp.ViewModel
{
    public class BlogCreateViewModel
    {
        [Required] 
        public string Title { get; set; } = null!;
        [Required]

        public string Content { get; set; } = null!;


        public string Tags { get; set; } = null!;
      

        
        
    }
}
