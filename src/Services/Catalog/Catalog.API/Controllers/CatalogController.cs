namespace Catalog.API.Controllers
{
  using Catalog.API.Entities;
  using Catalog.API.Extensions;
  using Catalog.API.Repositories;
  using Catalog.API.RequestHelpers;
  using Microsoft.AspNetCore.Mvc;
  using System.Net;
  using System.Text.Json;

  public class CatalogController : BaseApiController
  {
    private readonly IProductRepository _repository;
    private readonly ILogger<CatalogController> _logger;

    public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
    {
      _repository = repository ?? throw new ArgumentNullException(nameof(repository));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedList<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PagedList<Product>>> GetProducts([FromQuery]ProductParams productParams)
    {
      var products = await _repository.GetProducts(productParams);
      Response.AddPaginationHeader(products.MetaData);
      return Ok(products);
    }

    [HttpGet("{id:length(24)}", Name = "GetProduct")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetProductById(string id)
    {
      var product = await _repository.GetProduct(id);
      if (product == null)
      {
        _logger.LogError($"Product with id: {id}, not found.");
        return NotFound();
      }
      return Ok(product);
    }

    [Route("[action]/{category}", Name = "GetProductByCategory")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
    {
      var products = await _repository.GetProductByCategory(category);
      return Ok(products);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
      await _repository.CreateProduct(product);

      return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProduct([FromBody] Product product)
    {
      return Ok(await _repository.UpdateProduct(product));
    }

    [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProductById(string id)
    {
      return Ok(await _repository.DeleteProduct(id));
    }

    [HttpGet("filters")]
    public  IActionResult GetFilters()
    {
      return Ok( _repository.GetFiltersCatalog());
    }
  }
}