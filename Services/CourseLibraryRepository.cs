using AT_API.App_Code;
using AT_API.Model_Action;
using AT_API.Models;
using AT_API.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AT_API.Services
{
    public class CourseLibraryRepository : ICourseLibraryRepository
    {
        private readonly WorkshopAPI _context;

        private readonly AppSettings _appSettings;
        public CourseLibraryRepository(WorkshopAPI context, IOptions<AppSettings>
appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value ?? throw new
ArgumentNullException(nameof(appSettings));
        }
        public bool AuthorExists(Guid? authorId)
        {
            if (authorId == null)
            {
                throw new ArgumentNullException(nameof(authorId));
            }
            return _context.Authors.Any(a => a.Id == authorId);
        }
        public void AddAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }
            _context.Authors.Add(author);
        }
        public Author GetAuthor(Guid? authorId)
        {
            if (authorId == null)
                throw new ArgumentNullException();
            var data = _context.Authors.SingleOrDefault(a => a.Id == authorId);
            return data;
        }
        public IEnumerable<Author> GetAuthors()
        {
            var data = _context.Authors.ToList();
            return data;
        }
        public IEnumerable<Course> GetCourse(Guid Id)
        {
            var data = _context.Courses.Where(c => c.AuthorId == Id).ToList();
            return data;
        }
        public IEnumerable<Course> GetCourse(int Id)
        {
            var data = _context.Courses.Where(c => c.Id == Id).ToList();
            return data;
        }
        public Course? GetCourseById(Guid? authId, int? courseId)
        {
            if (authId == null || courseId == null)
                throw new ArgumentNullException();
            var data = _context.Courses.Where(c => c.AuthorId == authId && c.Id == courseId).SingleOrDefault();
            return data;
        }
        public Course? GetCourseById(int? courseId)
        {
            if (courseId == null)
                throw new ArgumentNullException();
            var data = _context.Courses.Where(c => c.Id == courseId).SingleOrDefault();
            return data;
        }
        public IEnumerable<Author> GetAuthors(string mainCategory, string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(mainCategory) && string.IsNullOrWhiteSpace(searchQuery))
            {
                return GetAuthors();
            }
            var collection = _context.Authors as IQueryable<Author>;
            if (!string.IsNullOrWhiteSpace(mainCategory))
            {
                mainCategory = mainCategory.Trim();
                collection = collection.Where(a => a.MainCategory == mainCategory);
            }
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                collection = collection.Where(a => a.MainCategory.Contains(searchQuery)
                || a.FirstName.Contains(searchQuery)
                || a.LastName.Contains(searchQuery));
            }
            return collection.ToList();
        }
        public void AddCourse(Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }
            _context.Courses.Add(course);
        }
        public void UpdateCourse(Course course)
        {
            var data = _context.Courses.SingleOrDefault(c => c.Id == course.Id);
            data.Title = course.Title;
            data.Description = course.Description;
            data.AuthorId = course.AuthorId;
            _context.Courses.Update(data);
        }
        public void UpdateAuthor(Author author)
        {
            var data = _context.Authors.SingleOrDefault(a => a.Id == author.Id);
            data.FirstName = author.FirstName;
            data.LastName = author.LastName;
            data.MainCategory = author.MainCategory;
            data.DateOfBirth = author.DateOfBirth;
            _context.Authors.Update(data);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void DeleteAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }
            _context.Authors.Remove(author);
        }
        public void DeleteCourse(Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }
            _context.Courses.Remove(course);
        }
        public User Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == username && x.Password == password);
            if (user == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key), Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = "";
            return user;
        }
        public bool IsUniqueUser(string username)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == username);
            if (user == null)
                return true;
            return false;
        }
        public User Register(string username, string password)
        {
            User user = new User
            {
                Username = username,
                Password = password,
                Role = "user"
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            user.Password = "";
            return user;

        }
    }
}
