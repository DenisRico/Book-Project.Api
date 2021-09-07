using Microsoft.EntityFrameworkCore;
using MyProject.Common.Models;
using MyProject.Common.Models.Domain;
using MyProject.Common.Repositories;
using MyProject.Dal.SqlContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Dal.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly MyProjectContext _dbContext;
        public BookRepository(MyProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Book>> GetOdered()
        {
            var books = await _dbContext.Books.ToListAsync();
            var genres =await _dbContext.Genres.ToListAsync();

            if (genres.Count()==0)
            {
                _dbContext.Genres.AddRange(
                    new Genre { Name = "Horror" },
                    new Genre { Name = "Comedy" },
                    new Genre { Name = "Drama" },
                    new Genre { Name = "Other" }
                    );
                await _dbContext.SaveChangesAsync();
            }


            var raw = await _dbContext.Books.Include(item=>item.Genres)
               .AsNoTracking().ToListAsync();
            //if (raw.Count == 0)
            //{
            //    raw.Add(new Position { Id = 1, Name = "Brihhton", Lat1 = 25.774, Lng1 = -80.19, Lat2 = 18.466, Lng2 = -66.118, Lat3 = 32.321, Lng3 = -64.757, Lat4 = 34.321, Lng4 = -65.757 });
            //    raw.Add(new Position { Id = 2, Name = "Brihh", Lat1 = 29.774, Lng1 = -85.19, Lat2 = 23.466, Lng2 = -70.118, Lat3 = 36.321, Lng3 = -68.757, Lat4 = 38.321, Lng4 = -68.757 });
            //}
            return raw;
        }

        public async Task Add(Book book, int[] arrayOfGenres)
        {
            var context = _dbContext;

            var listOfGenres = new List<Genre>();

            foreach (int i in arrayOfGenres)
            {
                var obj = context.Genres.FirstOrDefault(item => item.Id.Equals(i));
                //book.Genres.Add(obj);
                listOfGenres.Add(obj);
            }
            book.Genres = listOfGenres;
            context.Books.Add(book);

            //var c = context.Genres.FirstOrDefault(item => item.Id.Equals(book.));
            //c.Books.Add(book);

            //List<Genre> listWithGenres = new List<Genre>();

            //foreach (int id in arrayOfGenres)
            //{
            //    listWithGenres.Add(new Genre
            //    {
            //        Books= book
            //    });
            //}

            //var genre = context.Genres;

            //foreach (var item in listWithGenres)
            //{
            //    genre.Add(item);
            //}

            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Book> Delete(int id)
        {

            using var context = _dbContext;

            var entity = await context.Books.FirstOrDefaultAsync(item => item.Id.Equals(id));

            //if (entity == null) return;

            context.Books.Remove(entity);

            await context.SaveChangesAsync().ConfigureAwait(false);

            return entity;
        }

        public async Task UpdatePosition(Book book)
        {
            using var context = _dbContext;
            var entity = await context.Books.FirstOrDefaultAsync(item => item.Id.Equals(book.Id));

            if (book == null)
            {
                return;
            }

            entity.Name = book.Name;
            entity.Author = book.Author;
            


            context.Books.Update(entity);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Book> GetById(int id)
        {
            using var context = _dbContext;

            var entity = await context.Books
                .Include(item=>item.Genres)
                .FirstOrDefaultAsync(item => item.Id.Equals(id));

            return entity;
        }
    }
}
