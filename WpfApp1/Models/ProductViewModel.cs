namespace WpfApp1.Models
{
    public class ProductViewModel
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public string Picture { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Name}  Price: {Price}";
        }
    }
}