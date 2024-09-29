using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace đatn.Models
{
    public class User
    {
        public int user_id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string user_image { get; set; } 
        public string phone_number { get; set; } 
        public int Age { get; set; } 
        public string Gender { get; set; } 
        public string Role { get; set; } 
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;

        /*public ICollection<Property>? Properties { get; set; }
        public ICollection<Message>? SentMessages { get; set; }
        public ICollection<Message>? ReceivedMessages { get; set; }*/
    }
}
