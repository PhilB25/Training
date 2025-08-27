using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AT_API.Model_Action
{
    public class Course_res
    {

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public Guid AuthorId { get; set; }

    }
}
