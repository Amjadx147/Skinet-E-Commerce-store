using System;
using Core.Entities;

namespace Core.InterFaces;

public interface ICartService
{
    Task<ShoppingCart?> GetCartAsync(string key);

    Task<ShoppingCart?> SetCartAsync(ShoppingCart cart);

    Task<bool> DeleteCartAsync(string key);



}
