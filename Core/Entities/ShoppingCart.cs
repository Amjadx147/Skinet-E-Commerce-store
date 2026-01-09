using System;

namespace Core.Entities;

public class ShoppingCart
{

    public required string id { get; set; }

    public List<CartIteam> Items { get; set; } = [];



}
