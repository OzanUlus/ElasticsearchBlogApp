using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticsearchBlogApp.Models;
using ElasticsearchBlogApp.ViewModel;

namespace ElasticsearchBlogApp.Repository
{
    public class ECommerceRepository
    {
        private readonly ElasticsearchClient _elasticsearchClient;

        public ECommerceRepository(ElasticsearchClient elasticsearchClient)
        {
            _elasticsearchClient = elasticsearchClient;
        }

        private const string indexName = "kibana_sample_data_ecommerce";
        public async Task<(List<ECommerce> list ,long count)> SearchAsync(ECommerceViewModel eCommerceViewModel,int page,int pageSize) 
        {
            List<Action<QueryDescriptor<ECommerce>>> listQuery = new();

            if (!string.IsNullOrEmpty(eCommerceViewModel.Category)) 
            {
                listQuery.Add(q => q
                .Match(m => m
                .Field(f => f.Category)
                .Query(eCommerceViewModel.Category)));
            }
            if (!string.IsNullOrEmpty(eCommerceViewModel.CustomerFullName))
            {
                listQuery.Add(q => q
                .Match(m => m
                .Field(f => f.CustomerFullName)
                .Query(eCommerceViewModel.CustomerFullName)));
            }
            if (eCommerceViewModel.OrderDateStart.HasValue)
            {
                listQuery.Add(q => q
                .Range(r => r
                .DateRange(dr => dr
                .Field(f => f.OrderDate)
                .Gte(eCommerceViewModel.OrderDateStart.Value))));
            }
            if (eCommerceViewModel.OrderDateEnd.HasValue)
            {
                listQuery.Add(q => q
                .Range(r => r
                .DateRange(dr => dr
                .Field(f => f.OrderDate)
                .Lte(eCommerceViewModel.OrderDateEnd.Value))));
            }
            if (string.IsNullOrEmpty(eCommerceViewModel.Gender)) 
            {
                listQuery.Add(q => q
                .Term(t => t
                .Field(f => f.Gender)
                .Value(eCommerceViewModel.Gender)));
            }

            var pageFrom = (page - 1) * pageSize;

            var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Size(pageSize).From(pageFrom)
            .Query(q => q
            .Bool(b => b
            .Must(listQuery.ToArray()))));

            foreach (var hit in result.Hits) hit.Source.Id = hit.Id;

            return (list: result.Documents.ToList(), result.Total);
        }
    }
}
