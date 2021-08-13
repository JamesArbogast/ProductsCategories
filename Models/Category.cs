using System;
using System.ComponentModel.DataAnnotations;

namespace ProductsCategories.Models
  {
      public class Category
      {
        [Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "is required")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters")]
        [Display(Name = "First Name")]
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
      }
  }