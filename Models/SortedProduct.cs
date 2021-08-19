using System.ComponentModel.DataAnnotations;
using ProductsCategories.Models;
using System;

public class SortedProduct
{
    [Key]
    public int SortedProductId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}