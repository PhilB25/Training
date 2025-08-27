using AT_API.App_Code;
using AT_API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AT_API.Services
{
    public class AuthorService
    {
        private readonly WorkshopAPI _context;

        public AuthorService(WorkshopAPI context)
        {
            _context = context;
          
        }
        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }
        public async Task<Author> ReadByID(Guid id)
        {

                var auth = await _context.Authors.Where(t => t.Id == id).FirstOrDefaultAsync();
                return auth;

            
        }
        public async Task<Author> CreateAuthorAsync(AuthorCreateDto req)
        {
            var author = new Author
            {
                Id = Guid.NewGuid(),
                FirstName = req.FirstName,
                LastName = req.LastName,
                DateOfBirth = req.DateOfBirth,
                MainCategory = req.MainCategory
            };
            _context.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }
    }
}
