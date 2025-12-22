namespace GameStore.Models {
    public class Product {
        public int Id { get; set; }
        public int ProductIdForUser { get; set; }
        public int Count { get; set;}
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Price { get; set; } = null!;  
    }
}