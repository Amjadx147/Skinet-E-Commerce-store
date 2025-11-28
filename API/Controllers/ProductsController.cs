using System;
using API.RequstHelper;
using Core.Entities;
using Core.InterFaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;



public class ProductsController(IGenericRepository<Product> repo) : BaseApiController
{


    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProduct
    ([FromQuery]ProductSpecParams specParams)
    {

        var spec = new ProductSpecification(specParams);

        return await CreatePagedResult(repo, spec, specParams.PageIndex, specParams.PageSize);
    }

    [HttpGet("{id:int}")]

    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var Product = await repo.GetIdAsync(id);

        if (Product == null) return NotFound();

        return Product; 
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.Add(product);

        if (await repo.SaveAllAsync())
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

        repo.Update(product);

        if( await repo.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem Upditing Product"); 
        
    }

    [HttpDelete("{id:int}")]


     public async Task<ActionResult> DelectProduct(int id  )
    {

        var Product = await repo.GetIdAsync(id);

        if (Product == null) return NotFound();

        repo.Remove(Product); 

      if( await repo.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem Deleting the Product"); 
        
    }



     [HttpGet("brands")]


     public async Task<ActionResult<IReadOnlyList<String>>> GetBrand()
    {

       var spec = new BrandListSpecification();

        return Ok(await repo.ListAsync(spec)); 
        
    }


    
     [HttpGet("types")]


     public async Task<ActionResult<IReadOnlyList<String>>> GetTypes()
    {

        var spec = new TypeListSpecification();

        return Ok(await repo.ListAsync(spec)); 
        
    }



    private bool ProductExists(int id)
    {
        return repo.Exists(id);
    }
}

