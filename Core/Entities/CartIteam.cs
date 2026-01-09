using System;

namespace Core.Entities;

public class CartIteam
{
     public  int Productid { get; set; }

    public required string ProductName  { get; set; } 

    public  decimal Price { get; set; }

    public  int Quantity { get; set; }

    public required string PictureUrl { get; set; }

     public required string Brand  { get; set; }

      public required string Type  { get; set; }
}
