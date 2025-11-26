using System;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProductsController : ControllerBase
{

    private readonly StorContext context;

    public ProductsController(StorContext context)
    {
        
        this.context = context;
        
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
    {
        return await context.Products.ToListAsync(); 
    }

    [HttpGet("{id:int}")]

    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var Product = await context.Products.FindAsync(id);

        if (Product == null) return NotFound();

        return Product; 
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        context.Products.Add(product); 

        await context.SaveChangesAsync(); 

        return product; 
    }

    [HttpPut("{id:int}")]

    public async Task<ActionResult> UpdateProduct(int id , Product product )
    {

        if (product.Id  != id || !ProductExists(id) ) 
        return BadRequest("Cannot   update this Product ");

        context.Entry(product).State = EntityState.Modified;

        await context.SaveChangesAsync();

        return NoContent(); 
        
    }

    [HttpDelete("{id:int}")]


     public async Task<ActionResult> DelectProduct(int id  )
    {

        var Product = await context.Products.FindAsync(id);

        if (Product == null) return NotFound();

        context.Products.Remove(Product);

        await context.SaveChangesAsync(); 

        return NoContent();
        
    }

    private bool ProductExists(int id)
    {
        return context.Products.Any(x => x.Id == id);
    }
}

