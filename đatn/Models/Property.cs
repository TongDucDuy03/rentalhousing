namespace đatn.Models
{
    public class Property
    {
        public int PropertyId { get; set; }
        public int OwnerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public float Area { get; set; }
        public int RoomCount { get; set; }
        public string Amenities { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public User Owner { get; set; }
        public ICollection<PropertyImage> PropertyImages { get; set; }
    }
}
