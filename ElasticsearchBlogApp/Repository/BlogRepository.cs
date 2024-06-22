using Elastic.Clients.Elasticsearch;
using ElasticsearchBlogApp.Models;

namespace ElasticsearchBlogApp.Repository
{
    public class BlogRepository
    {
        private readonly ElasticsearchClient _elasticsearchClient;
        private string indexName = "blog";

        public BlogRepository(ElasticsearchClient elasticsearchClient)
        {
            _elasticsearchClient = elasticsearchClient;
        }

        public async Task<Blog?> SaveAsync(Blog newBlog) 
        {
          newBlog.Created = DateTime.Now;
            var response = await _elasticsearchClient.IndexAsync(newBlog, b =>b.Index(indexName));
            if (!response.IsValidResponse) return null;
            newBlog.Id = response.Id;
            return newBlog;
            
        
        }
        public async Task<List<Blog>> SearchAsync(string searchText) 
        {
            //title --> full text
            //content --> full text

            var result = await _elasticsearchClient.SearchAsync<Blog>(s => s
            .Index(indexName).Size(1000).Query(q => q
            .Bool(b => b
            .Should(
                sh => sh.Match(m => m.Field(f => f.Content).Query(searchText)), 
                sh => sh.MatchBoolPrefix(mb => mb.Field(f => f.Title).Query(searchText))))));

            foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
            return result.Documents.ToList();
        }
    }
}
