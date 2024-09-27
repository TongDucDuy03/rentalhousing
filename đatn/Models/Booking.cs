namespace đatn.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int PropertyId { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = "đang chờ"; // "đang chờ", "đã xác nhận", "đã hủy"
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Relationships
        public Property Property { get; set; }
        public User User { get; set; }
    }
}
