using System;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContectSeed
{

    public static async Task SeedAsync(StorContext context)
    {
        if(!context.Products.Any())
        {
            var ProductsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/Products.json");

            var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

            if(Products == null) return;

            context.Products.AddRange(Products);

            await context.SaveChangesAsync();
        }
    }

}
