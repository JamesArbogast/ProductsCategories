using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsCategories.Models
  {
      public class Product
      {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "is required")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters")]
        [Display(Name = "First Name")]
        public string Name { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "must be at least 10 characters")]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public List<SortedProduct> SortedProducts { get; set; }
      }
  }
