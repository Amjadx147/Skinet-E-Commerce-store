using System;
using Core.Entities;
using Core.InterFaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProductsController(IProductRepository repo) : ControllerBase
{


    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProduct(string? brand , string? type, string? sort)
    {
        return Ok(await repo.GetProductsAsync(brand, type,sort));
    }

    [HttpGet("{id:int}")]

    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var Product = await repo.GetProductByIDAsync(id);

        if (Product == null) return NotFound();

        return Product; 
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.AddProduct(product);

        if (await repo.SaveChangeAsync())
        {
            return CreatedAtAction("GetProduct ", new {id = product.Id}, product);


        }

        return BadRequest("Problem Creaating Product"); 
    }

    [HttpPut("{id:int}")]

    public async Task<ActionResult> UpdateProduct(int id , Product product )
    {

        if (product.Id  != id || !ProductExists(id) ) 
        return BadRequest("Cannot   update this Product ");

        repo.UpdateProduct(product);

        if( await repo.SaveChangeAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem Upditing Product"); 
        
    }

    [HttpDelete("{id:int}")]


     public async Task<ActionResult> DelectProduct(int id  )
    {

        var Product = await repo.GetProductByIDAsync(id);

        if (Product == null) return NotFound();

        repo.DeleteProduct(Product); 

      if( await repo.SaveChangeAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem Deleting the Product"); 
        
    }



     [HttpGet("brands")]


     public async Task<ActionResult<IReadOnlyList<String>>> GetBrand()
    {

       

        return Ok(await repo.GetBrandsAsync()); 
        
    }


    
     [HttpGet("types")]


     public async Task<ActionResult<IReadOnlyList<String>>> GetTypes()
    {

       

        return Ok(await repo.GetTypesAsync()); 
        
    }



    private bool ProductExists(int id)
    {
        return repo.ProductExists(id);
    }
}

