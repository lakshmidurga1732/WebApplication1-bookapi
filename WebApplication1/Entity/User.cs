using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entity
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(20)]
        public string Role { get; set; }
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(255)]
        public string Address { get; set; }
    }
}
