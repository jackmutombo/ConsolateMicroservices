namespace Shopping.Aggregator.Services
{
  using Shopping.Aggregator.Models;

  public interface ICatalogService
  {
    Task<IEnumerable<CatalogModel>> GetCatalog();
    Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category);
    Task<CatalogModel> GetCatalog(string id);
  }
}
