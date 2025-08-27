using System.ComponentModel.DataAnnotations;

namespace AT_API.Models
{
    public class Author 
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; } = null!;
        [StringLength(100)]
        public string ?LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        [StringLength(255)]
        public string ?MainCategory { get; set; }
    }
    public class AuthorCreateDto
    {

        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string? MainCategory { get; set; }
    }
    public class AuthorDto
    {
        public Guid? Id { get; set; } 
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
        public string? MainCategory { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
    }
}
