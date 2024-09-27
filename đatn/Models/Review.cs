namespace đatn.Models
{
    using System;

    public class Review
    {
        public int ReviewId { get; set; }
        public int PropertyId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; } // Between 1 and 5
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Relationships
        public Property Property { get; set; }
        public User User { get; set; }
    }

}
