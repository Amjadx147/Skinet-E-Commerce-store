using System;
using Core.Entities;
using Core.InterFaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CartController(ICartService cartService) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<ShoppingCart>> GetCartByid(string id)
    {
        var cart = await cartService.GetCartAsync(id); 

        return Ok(cart ?? new ShoppingCart{id = id});
    }

    [HttpPost]
    public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart)
    {
        var UpdateCart = await cartService.SetCartAsync(cart); 

        if ( UpdateCart == null ) return BadRequest("Problem with cart");
        
        return UpdateCart;
    }

    [HttpDelete]
    public async Task<ActionResult<ShoppingCart>> DeleteCart(string id)
    {
        var result = await cartService.DeleteCartAsync(id); 

        if ( !result ) return BadRequest("Problem deleting cart");
        
        return Ok();
    }
    
}
