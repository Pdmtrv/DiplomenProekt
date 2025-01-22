using System.ComponentModel.DataAnnotations;

namespace TheRockShoppApp.Models.Manufacturer
{
    public class ManufacturerPairVM
    {
        public int Id { get; set; }
        [Display(Name = "Manufacturer")]
        public string Name { get; set; } = null!;
    }
}
