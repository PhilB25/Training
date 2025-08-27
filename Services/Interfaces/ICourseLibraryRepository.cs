using AT_API.Models;

namespace AT_API.Services.Interfaces
{
    public interface ICourseLibraryRepository
    {
        IEnumerable<Author> GetAuthors();
        Author GetAuthor(Guid? authorId);
        public bool AuthorExists(Guid? authorId);
        IEnumerable<Author> GetAuthors(string mainCategory, string searchQuery);
        void AddAuthor(Author author);
        void AddCourse(Course course);
        void Save();
        IEnumerable<Course> GetCourse(Guid Id);
        IEnumerable<Course> GetCourse(int Id);
        void UpdateAuthor(Author author);
        Course? GetCourseById(Guid? authId,int? courseId);
        Course? GetCourseById(int? courseId);
        void UpdateCourse(Course course);
        void DeleteAuthor(Author author); void DeleteCourse(Course course);
        bool IsUniqueUser(string username);
        User Authenticate(string username, string password);
        User Register(string username, string password);
    }
}
