using System.ComponentModel.DataAnnotations;

namespace TheRockShoppApp.Models.Product
{
    public class ProductDeleteVM
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = null!;
        public int CategoryId { get; set; }  
        [Display(Name = "Category")]
        public string CategoryName { get; set; } = null!;
        public int ManufacturerId { get; set; } 
        [Display(Name = "Manufacturer")]
        public string ManufacturerName { get; set; } = null!;
        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; } = null!;
        [Display(Name = "Picture")]
        public string Picture { get; set; } = null!;
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name = "Discount")]
        public decimal Discount
        {
            get; set;
        }
    }
}
