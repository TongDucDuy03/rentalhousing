using Microsoft.EntityFrameworkCore;

namespace đatn.Models
{
    public class RentalHousingDbContext : DbContext
    {
        public RentalHousingDbContext(DbContextOptions<RentalHousingDbContext> options) : base(options)
        {
        }

        // Khai báo các DbSet tương ứng với các bảng trong cơ sở dữ liệu
        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Message> Messages { get; set; }

        // Tùy chọn cấu hình thêm các mối quan hệ hoặc ràng buộc
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationship configurations nếu cần thiết
            modelBuilder.Entity<User>()
                .HasMany(u => u.SentMessages)
                .WithOne(m => m.Sender)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.ReceivedMessages)
                .WithOne(m => m.Receiver)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
