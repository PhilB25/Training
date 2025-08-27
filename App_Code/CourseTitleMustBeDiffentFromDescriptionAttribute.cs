using AT_API.Model_Action;
using System.ComponentModel.DataAnnotations;

namespace AT_API.App_Code
{
    public class CourseTitleMustBeDiffentFromDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var course = (Course_req)validationContext.ObjectInstance;
            if (course.Title == course.Description)
            {
                return new ValidationResult(
                "The provided description should be different from the title. jjbb",
                new[] { "CourseForCreationDto" });
            }
            return ValidationResult.Success;
        }
    }

}
