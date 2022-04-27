using Catalog.API.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Catalog.API.Extensions
{
  public static class ProductExtentions
  {

    public static IQueryable<Product> SortByLinq(this IQueryable<Product> query, string orderBy)
    {
      if (string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(p => p.Name);

      return orderBy switch
      {
        "price" => query.OrderBy(p => p.Price),
        "priceDesc" => query.OrderByDescending(p => p.Price),
        _ => query.OrderBy(p => p.Name)
      };
    }

    

    public static IAggregateFluent<Product> SortByLinq(this IAggregateFluent<Product> query, string orderBy)
    {
      if (string.IsNullOrWhiteSpace(orderBy)) return query.SortBy(p => p.Name);

      return orderBy switch
      {
        "price" => query.SortBy(p => p.Price),
        "priceDesc" => query.SortByDescending(p => p.Price),
        _ => query.SortBy(p => p.Name)
      };
    }
    public static IQueryable<Product> SearchTermLinq(this IQueryable<Product> query, string searchTerm)
    {
      if (string.IsNullOrWhiteSpace(searchTerm)) return query;

      var lowerCaseSearch = searchTerm.Trim().ToLower();
      
      return query.Where(p => p.Name.ToLower().Contains(lowerCaseSearch));
    }

    public static IAggregateFluent<Product> SearchTermLinq(this IAggregateFluent<Product> query, string searchTerm)
    {
      if (string.IsNullOrWhiteSpace(searchTerm)) return query;

      var lowerCaseSearch = searchTerm.Trim().ToLower();

      return query.Match(p => p.Name.ToLower().Contains(lowerCaseSearch));
    }



    public static IQueryable<Product> FilterLinq(this IQueryable<Product> query, string brands, string types)
      {
        var brandList = new List<string>();
        var typesList = new List<string>();


        if (!string.IsNullOrWhiteSpace(brands))
        {
          brandList.AddRange(brands.Split(",").ToList());
          foreach(var brand in brandList)
          {
            query = query.Where(p => p.Brand.Contains(brand));
          }
        }

        if (!string.IsNullOrWhiteSpace(types))
        {
          typesList.AddRange(types.Split(",").ToList());
          foreach (var type in typesList)
          {
            query = query.Where(p => p.Type.Contains(type));
          }
        //query = query.Where(p => typesList.Count == 0 || typesList.Contains(p.Type.ToLower()));
      }

      return query;
    }

    public static IAggregateFluent<Product> FilterLinq(this IAggregateFluent<Product> query, string brands, string types)
    {

      if (!string.IsNullOrWhiteSpace(brands))
      {
        var brandArray = brands.Trim().Split(",");
        var brandBuild = Builders<Product>.Filter.In("Brand", brandArray);
        query = query.Match(brandBuild);
      }

      if (!string.IsNullOrWhiteSpace(types))
      {
        var typeArray = types.Trim().Split(",");
        var typeBuild = Builders<Product>.Filter.In("Type", typeArray);
        query = query.Match(typeBuild);
      }

      return query;
    }


  }
}
