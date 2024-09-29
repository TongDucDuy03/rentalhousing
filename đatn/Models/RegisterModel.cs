namespace đatn.Models
{
    public class RegisterModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Vai trò (tuỳ chọn)
        public string UserImage { get; set; } // Đường dẫn ảnh
        public string PhoneNumber { get; set; } // Số điện thoại
        public int Age { get; set; } // Tuổi
        public string Gender { get; set; } // Giới tính
    }
}
