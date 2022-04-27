using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Catalog.API.RequestHelpers
{
  public class PagedList<T> : List<T>
  {
    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
      MetaData = new MetaData
      {
        CurrentPage = pageNumber,
        PageSize = pageSize,
        TotalCount = count,
        TotalPages = (int)Math.Ceiling(count / (double)pageSize),
      };
      AddRange(items);
    }

    public MetaData MetaData { get; set; }

    public static async Task<PagedList<T>> ToPagedList(IQueryable<T> query, int pageNumber, int pageSize)
    {
      int count = await Task.FromResult(query.Count());

      var items = await Task.FromResult(query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
      return new PagedList<T>(items, count, pageNumber, pageSize);

    }

    public static async Task<PagedList<T>> ToPagedList(IAggregateFluent<T> query, int pageNumber, int pageSize)
    {
      int count = (await Task.FromResult(query.ToEnumerable())).Count();
      var items = await Task.FromResult(query.ToEnumerable().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());

      return new PagedList<T>(items, count, pageNumber, pageSize);

    }

    //public static async Task<PagedList<T>> ToPagedList(IAggregateFluent<T> query, int pageNumber, int pageSize)
    //{

    //  int count = 11;
    //  var items = await Task.FromResult(query.ToEnumerable().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());



    //  return new PagedList<T>(items, count, pageNumber, pageSize);

    //}
  }
}
