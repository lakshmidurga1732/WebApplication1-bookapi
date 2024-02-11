using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Entity
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [StringLength(255)]
 
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Author { get; set; }

        [Required]
        [StringLength(50)]
        public string Genre { get; set; }

        [Required]
        [StringLength(13)]
        public string ISBN { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }
    }
}
