using Core.Context;
using Core.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure
{
    public class StoreSeedData
    {
        public static async Task SeedDataAsync(StoreAppDb context, ILoggerFactory loggerFactory)
        {
			try
			{
				if(context.ProductBrands !=null && !context.ProductBrands.Any())
				{
					var BrandData = File.ReadAllText("../Infrastructure/SeedData/brands.json");
					var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);

					if(Brands != null)
					{
						foreach (var brand in Brands)
							await context.ProductBrands.AddAsync(brand);
						await context.SaveChangesAsync();
                    }
				}

                if (context.ProductTypes != null && !context.ProductTypes.Any())
                {
                    var TypeData = File.ReadAllText("../Infrastructure/SeedData/types.json");
                    var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);

                    if (Types != null)
                    {
                        foreach (var type in Types)
                            await context.ProductTypes.AddAsync(type);
                        await context.SaveChangesAsync();
                    }
                }

                if (context.Products != null && !context.Products.Any())
                {
                    var ProductData = File.ReadAllText("../Infrastructure/SeedData/products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);

                    if (Products != null)
                    {
                        foreach (var product in Products)
                            await context.Products.AddAsync(product);
                        await context.SaveChangesAsync();
                    }
                }
                if (context.DeliveryMethods != null && !context.DeliveryMethods.Any())
                {
                    var DeliveryMethodData = File.ReadAllText("../Infrastructure/SeedData/delivery.json");
                    var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodData);

                    if (DeliveryMethods != null)
                    {
                        foreach (var deliveryMethod in DeliveryMethods)
                            await context.DeliveryMethods.AddAsync(deliveryMethod);
                        await context.SaveChangesAsync();
                    }
                }
            }
			catch (Exception ex)
			{
                var logger = loggerFactory.CreateLogger<StoreSeedData>();
                logger.LogError(ex.Message);
			}
        }
    }
}
