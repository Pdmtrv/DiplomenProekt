using System.ComponentModel.DataAnnotations;
using TheRockShoppApp.Models.Category;
using TheRockShoppApp.Models.Manufacturer;

namespace TheRockShoppApp.Models.Product
{
    public class ProductEditVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Product Name")]
        public string Name { get; set; } = null!;
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public virtual List<CategoryPairVM> Categories { get; set; } = new List<CategoryPairVM>();
        [Required]
        [Display(Name = "Manufacturer")]
        public int ManufacturerId { get; set; }
        public virtual List<ManufacturerPairVM> Manufacturer { get; set; } = new List<ManufacturerPairVM>();
        [Display(Name = " Description")]
        public string ProductDescription { get; set; } = null!;
        [Display(Name = "Picture")]
        public string Picture { get; set; } = null!;
        [Range(0, 100)]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name = "Discount")]
        public decimal Discount { get; set; }
    }
}
