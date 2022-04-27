using Catalog.API.Data;
using Catalog.API.Entities;
using Catalog.API.Extensions;
using Catalog.API.RequestHelpers;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
  public class ProductRepository : IProductRepository
  {
    private readonly ICatalogContext _context;

    public ProductRepository(ICatalogContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
      return await _context
                      .Products
                      .Find(p => true)
                      .ToListAsync();
    }

    public async Task<PagedList<Product>> GetProducts(ProductParams productParams)
    {

      //var query = _context.Products.AsQueryable()
      //    .SortByLinq(productParams.OrderBy)
      //    .SearchTermLinq(productParams.SearchTerm)
      //    .FilterLinq(productParams.Brands, productParams.Types);




      var query = _context.Products.Aggregate()
          .SortByLinq(productParams.OrderBy)
          .SearchTermLinq(productParams.SearchTerm)
          .FilterLinq(productParams.Brands, productParams.Types);

      var products =  await PagedList<Product>.ToPagedList(query, productParams.PageNumber, productParams.PageSize);

      return products;
    }



    public Task<Product> GetProduct(string id)
    {
      return _context
                     .Products
                     .Find(p => p.Id == id)
                     .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByName(string name)
    {
      FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

      return await _context
                      .Products
                      .Find(filter)
                      .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
    {
      FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

      return await _context
                      .Products
                      .Find(filter)
                      .ToListAsync();
    }

    public Task CreateProduct(Product product)
    {
      return _context.Products.InsertOneAsync(product);
    }

    public async Task<bool> UpdateProduct(Product product)
    {
      var updateResult = await _context
                                  .Products
                                  .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

      return updateResult.IsAcknowledged
              && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
      FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

      DeleteResult deleteResult = await _context
                                          .Products
                                          .DeleteOneAsync(filter);

      return deleteResult.IsAcknowledged
          && deleteResult.DeletedCount > 0;
    }

    public Object GetFiltersCatalog()
    {
      var brands = _context.Products.AsQueryable().Select(e => e.Brand).Distinct().ToList();
      var types = _context.Products.AsQueryable().Select(e => e.Type).Distinct().ToList();
      var storeNames = _context.Products.AsQueryable().Select(e => e.Store.StoreName).Distinct().ToList();
      var storeLocations = _context.Products.AsQueryable().Select(e => e.Store.Address).Distinct().ToList();
      //var brands = await _context.Products.Distinct<string>("brand", FilterDefinition<Product>.Empty).ToListAsync();
      //var types = await _context.Products.Distinct<string>("type", FilterDefinition<Product>.Empty).ToListAsync();
      //var storeName = await _context.Products.Distinct<string>("store.storeName", FilterDefinition<Product>.Empty).ToListAsync();
      //var storeLocation = await _context.Products.Distinct<string>("store.address", FilterDefinition<Product>.Empty).ToListAsync();

      return new { brands, types, storeNames, storeLocations };
      //return new {b};
                     
    }
  }
}