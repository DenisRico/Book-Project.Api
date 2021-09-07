using Microsoft.EntityFrameworkCore;
using MyProject.Common.Models.Domain;
using MyProject.Common.Repositories;
using MyProject.Dal.SqlContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Dal.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly MyProjectContext _dbContext;
        public GenreRepository(MyProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Genre>> GetOdered(int id)
        {
            var books = await _dbContext.Books.ToListAsync();
            var genres = await _dbContext.Genres.ToListAsync();

            var raw = await _dbContext.Genres.Include(item => item.Books)
               .AsNoTracking().ToListAsync();
           
            return raw;
        }

        public async Task<Genre> GetById(int id)
        {
            var books = await _dbContext.Books.ToListAsync();
            var genres = await _dbContext.Genres.ToListAsync();

            var entity = await _dbContext.Genres.Include(item => item.Books).FirstOrDefaultAsync(item => item.Id.Equals(id));

            return entity;
        }
    }
}
