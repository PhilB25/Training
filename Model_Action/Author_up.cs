using System.ComponentModel.DataAnnotations;

namespace AT_API.Model_Action
{
    public class Author_up
    {
        public string FirstName { get; set; } = null!;
        
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string ?MainCategory { get; set; }
    }
    
}
