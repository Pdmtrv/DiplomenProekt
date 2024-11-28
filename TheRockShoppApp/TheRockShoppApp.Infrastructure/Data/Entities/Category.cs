using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheRockShoppApp.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;
        public virtual IEnumerable<Product> Productss { get; set;} =  new List<Product>();
    }
}
