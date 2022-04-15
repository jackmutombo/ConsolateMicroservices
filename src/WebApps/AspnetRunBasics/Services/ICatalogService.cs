﻿namespace AspnetRunBasics.Services
{
  using AspnetRunBasics.Models;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  public interface ICatalogService
  {
    Task<IEnumerable<CatalogModel>> GetCatalog();
    Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category);
    Task<CatalogModel> GetCatalog(string id);
    Task<CatalogModel> CreateCatalog(CatalogModel model);
  }
}