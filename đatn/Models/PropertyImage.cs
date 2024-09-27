namespace đatn.Models
{
    public class PropertyImage
    {
        public int ImageId { get; set; }
        public int PropertyId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Relationship
        public Property Property { get; set; }
    }
}
