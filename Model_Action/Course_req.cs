using AT_API.App_Code;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AT_API.Model_Action
{
    [CourseTitleMustBeDiffentFromDescription]
    public class Course_req 
    {
        [Required]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public Guid AuthorId { get; set; }

        
    }
    public class Course_up
    {
    
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public Guid AuthorId { get; set; }


    }
}
