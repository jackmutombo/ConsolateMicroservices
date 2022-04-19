namespace Catalog.API.Data
{
  using Catalog.API.Entities;
  using MongoDB.Driver;
  public class CatalogContextSeed
  {
    public static void SeedData(IMongoCollection<Product> productCollection)
    {
      bool existProduct = productCollection.Find(p => true).Any();
      if (!existProduct)
      {
        productCollection.InsertManyAsync(GetPreconfiguredProducts());
      }
    }

    private static IEnumerable<Product> GetPreconfiguredProducts()
    {
      return new List<Product>()
            {
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "IPhone X",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "/images/products/product-1.png",
                    Price = 95000L,
                    Category = "Smart Phone",
                    QuantityInStock = 120,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4710",
                      Address = "Lubumbashi",
                      StoreName = "SmartDevice"
                    }
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name = "Samsung 10",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "/images/products/product-2.png",
                    Price = 84000L,
                    Category = "Smart Phone",
                    QuantityInStock = 120,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4710",
                      Address = "Lubumbashi",
                      StoreName = "SmartDevice"
                    }
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f7",
                    Name = "Huawei Plus",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "/images/products/product-3.png",
                    Price = 65000L,
                    Category = "White Appliances",
                    QuantityInStock = 120,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4710",
                      Address = "Lubumbashi",
                      StoreName = "SmartDevice"
                    }
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f8",
                    Name = "Xiaomi Mi 9",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "/images/products/product-4.png",
                    Price = 47000L,
                    Category = "White Appliances",
                    QuantityInStock = 120,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4710",
                      Address = "Lubumbashi",
                      StoreName = "SmartDevice"
                    }
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f9",
                    Name = "HTC U11+ Plus",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "/images/products/product-5.png",
                    Price = 38000L,
                    Category = "Smart Phone",
                    QuantityInStock = 120,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4710",
                      Address = "Lubumbashi",
                      StoreName = "SmartDevice"
                    }
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47fa",
                    Name = "LG G7 ThinQ",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "/images/products/product-6.png",
                    Price = 24000L,
                    Category = "Home Kitchen",
                    QuantityInStock = 120,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4710",
                      Address = "Lubumbashi",
                      StoreName = "SmartDevice"
                    }
                },
                new Product
                {
                    Name = "Angular Speedster Board 2000",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 20000,
                    ImageFile = "/images/products/sb-ang1.png",
                    Brand = "Angular",
                    Type = "Boards",
                    QuantityInStock = 100,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4711",
                      Address = "Lubumbashi",
                      StoreName = "ReStore"
                    }
                },
                new Product
                {
                    Name = "Green Angular Board 3000",
                    Description = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.",
                    Price = 15000,
                    ImageFile = "/images/products/sb-ang2.png",
                    Brand = "Angular",
                    Type = "Boards",
                    QuantityInStock = 100,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4711",
                      Address = "Lubumbashi",
                      StoreName = "ReStore"
                    }
                },
                new Product
                {
                    Name = "Core Board Speed Rush 3",
                    Description =
                        "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                    Price = 18000,
                    ImageFile = "/images/products/sb-core1.png",
                    Brand = "NetCore",
                    Type = "Boards",
                    QuantityInStock = 100,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4711",
                      Address = "Lubumbashi",
                      StoreName = "ReStore"
                    }
                },
                new Product
                {
                    Name = "Net Core Super Board",
                    Description =
                        "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.",
                    Price = 30000,
                    ImageFile = "/images/products/sb-core2.png",
                    Brand = "NetCore",
                    Type = "Boards",
                    QuantityInStock = 100,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4711",
                      Address = "Lubumbashi",
                      StoreName = "ReStore"
                    }
                },
                new Product
                {
                    Name = "React Board Super Whizzy Fast",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 25000,
                    ImageFile = "/images/products/sb-react1.png",
                    Brand = "React",
                    Type = "Boards",
                    QuantityInStock = 100,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4711",
                      Address = "Lubumbashi",
                      StoreName = "ReStore"
                    }
                },
                new Product
                {
                    Name = "Typescript Entry Board",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 12000,
                    ImageFile = "/images/products/sb-ts1.png",
                    Brand = "TypeScript",
                    Type = "Boards",
                    QuantityInStock = 100,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4711",
                      Address = "Lubumbashi",
                      StoreName = "ReStore"
                    }
                },
                new Product
                {
                    Name = "Core Blue Hat",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1000,
                    ImageFile = "/images/products/hat-core1.png",
                    Brand = "NetCore",
                    Type = "Hats",
                    QuantityInStock = 100,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4711",
                      Address = "Lubumbashi",
                      StoreName = "ReStore"
                    }
                },
                new Product
                {
                    Name = "Green React Woolen Hat",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 8000,
                    ImageFile = "/images/products/hat-react1.png",
                    Brand = "React",
                    Type = "Hats",
                    QuantityInStock = 100,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4711",
                      Address = "Lubumbashi",
                      StoreName = "ReStore"
                    }
                },
                new Product
                {
                    Name = "Purple React Woolen Hat",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1500,
                    ImageFile = "/images/products/hat-react2.png",
                    Brand = "React",
                    Type = "Hats",
                    QuantityInStock = 100,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4711",
                      Address = "Lubumbashi",
                      StoreName = "ReStore"
                    }
                },
                new Product
                {
                    Name = "Blue Code Gloves",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1800,
                    ImageFile = "/images/products/glove-code1.png",
                    Brand = "VS Code",
                    Type = "Gloves",
                    QuantityInStock = 100,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4711",
                      Address = "Lubumbashi",
                      StoreName = "ReStore"
                    }
                },
                new Product
                {
                    Name = "Green Code Gloves",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1500,
                    ImageFile = "/images/products/glove-code2.png",
                    Brand = "VS Code",
                    Type = "Gloves",
                    QuantityInStock = 100,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4711",
                      Address = "Lubumbashi",
                      StoreName = "ReStore"
                    }
                },
                new Product
                {
                    Name = "Purple React Gloves",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1600,
                    ImageFile = "/images/products/glove-react1.png",
                    Brand = "React",
                    Type = "Gloves",
                    QuantityInStock = 100,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4711",
                      Address = "Lubumbashi",
                      StoreName = "ReStore"
                    }
                },
                new Product
                {
                    Name = "Green React Gloves",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1400,
                    ImageFile = "/images/products/glove-react2.png",
                    Brand = "React",
                    Type = "Gloves",
                    QuantityInStock = 100,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4711",
                      Address = "Lubumbashi",
                      StoreName = "ReStore"
                    }
                },
                new Product
                {
                    Name = "Redis Red Boots",
                    Description =
                        "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                    Price = 25000,
                    ImageFile = "/images/products/boot-redis1.png",
                    Brand = "Redis",
                    Type = "Boots",
                    QuantityInStock = 100,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4711",
                      Address = "Lubumbashi",
                      StoreName = "ReStore"
                    }
                },
                new Product
                {
                    Name = "Core Red Boots",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 18999,
                    ImageFile = "/images/products/boot-core2.png",
                    Brand = "NetCore",
                    Type = "Boots",
                    QuantityInStock = 100,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4711",
                      Address = "Lubumbashi",
                      StoreName = "ReStore"
                    }
                },
                new Product
                {
                    Name = "Core Purple Boots",
                    Description =
                        "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.",
                    Price = 19999,
                    ImageFile = "/images/products/boot-core1.png",
                    Brand = "NetCore",
                    Type = "Boots",
                    QuantityInStock = 100,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4711",
                      Address = "Lubumbashi",
                      StoreName = "ReStore"
                    }
                },
                new Product
                {
                    Name = "Angular Purple Boots",
                    Description = "Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.",
                    Price = 15000,
                    ImageFile = "/images/products/boot-ang2.png",
                    Brand = "Angular",
                    Type = "Boots",
                    QuantityInStock = 100,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4711",
                      Address = "Lubumbashi",
                      StoreName = "ReStore"
                    }
                },
                new Product
                {
                    Name = "Angular Blue Boots",
                    Description =
                        "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                    Price = 18000,
                    ImageFile = "/images/products/boot-ang1.png",
                    Brand = "Angular",
                    Type = "Boots",
                    QuantityInStock = 100,
                    Store = new ProductStore
                    {
                      Id = "602d2149e773f2a3990b4711",
                      Address = "Lubumbashi",
                      StoreName = "ReStore"
                    }
                },
            };
    }
  }
}