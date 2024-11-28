namespace TheRockShoppApp.Entities
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public virtual IEnumerable<Product> Productss { get; set; } = new List<Product>();
    }
}
