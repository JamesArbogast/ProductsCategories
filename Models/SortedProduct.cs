using System.ComponentModel.DataAnnotations;
using ProductsCategories.Models;

public class SortedProduct
{
    [Key]
    public int SortedProductId { get; set; }
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public Product Product { get; set; }
    public Category Category { get; set; }
}