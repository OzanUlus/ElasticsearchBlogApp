using ElasticsearchBlogApp.Repository;
using ElasticsearchBlogApp.ViewModel;

namespace ElasticsearchBlogApp.Services
{
    public class ECommerceService
    {
        private readonly ECommerceRepository _repository;

        public ECommerceService(ECommerceRepository repository)
        {
            _repository = repository;
        }
        public async Task<(List<ECommerceViewModel>, long totalCount, long pageLinkCount)> SearchAsync(ECommerceSearchViewModel searchViewModel, int page, int pageSize)
        {

            var (eCommerceList, totalCount) = await _repository.SearchAsync(searchViewModel, page, pageSize);

            var pageLinkCountCalculate = totalCount % pageSize;
            long pageLinkCount = 0;

            if (pageLinkCountCalculate == 0) pageLinkCount = totalCount / pageSize;
            else pageLinkCount = (totalCount / pageSize) + 1;

            var eCommerceListViewModel = eCommerceList.Select(e => new ECommerceViewModel
            {
                Category = String.Join(",", e.Category),
                CustomerFullName = e.CustomerFullName,
                CustomerFirstName = e.CustomerFirstName,
                CustomerLastName = e.CustomerLastName,
                OrderDate = e.OrderDate.ToShortDateString(),
                Gender = e.Gender.ToUpper(),
                Id = e.Id,
                OrderId = e.OrderId,
                TaxfulTotalPrice = e.TaxfulTotalPrice,

            }).ToList();
            return (eCommerceListViewModel, totalCount, pageLinkCount);
        }
    }
}
