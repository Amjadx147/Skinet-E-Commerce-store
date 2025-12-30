using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class CreatProductDTOs
{

    [Required]
    public  String Name { get; set; } = string.Empty;


    [Required]
    public  String Description { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be grater than 0 ")]
    public decimal Price { get; set; }


    [Required]
    public  String PictureUrl { get; set; } = string.Empty;

    [Required]
    public  String Brand { get; set; }= string.Empty;

    [Required]

    public  String Type { get; set; }= string.Empty;


    [Range(1, int.MaxValue, ErrorMessage = "Quantity is stock must be at least 1 ")]   
    public int QuantityInStock { get; set; }

}
