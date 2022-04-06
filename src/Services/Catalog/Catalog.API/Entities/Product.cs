using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Entities;

public class Product
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }

  [BsonElement("Name")]
  public string? Name { get; set; }
  public string? Category { get; set; }
  public string? Summary { get; set; }

  public string? Description { get; set; }
  public string? ImageFile { get; set; }
  public long Price { get; set; }
  public int QunatityInStock { get; set; }
  public string? Brand { get; set; }
  public string? Type { get; set; }
  public DateTime CreatedDate { get; set; } = DateTime.Now;
  public DateTime LastModified { get; set; } = DateTime.Now;
  public string? ModifiedBy { get; set; }
  public string? Warranty { get; set; }
  public bool IsTaxable { get; set; }
  public ProductOption? Option { get; set; }
  public ProductStore? Store { get; set; }

}

public class ProductOption
{
  public string? Size { get; set; }
  public string? Feature { get; set; }
  public string? Colour { get; set; }
  public string? ImageFile { get; set; }
  public int Quantity { get; set; }
}

public class ProductStore
{
  public string? Id { get; set; }
  public string? StoreName { get; set; }
  public string? Address { get; set; }
}

