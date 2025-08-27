using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AT_API.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Title { get; set; } = null!;
        [StringLength(1000)]
        public string? Description { get; set; }
        public Guid AuthorId { get; set; }
        //Navigation Properties
        [ForeignKey("AuthorId")]
        public Author? Author { get; set; }
    }
}
