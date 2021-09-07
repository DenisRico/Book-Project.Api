using MyProject.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyProject.Common.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetOdered();
        Task<Book> GetById(int id);
        Task Add(Book book, int[] arrayOfGenres);
        Task<Book> Delete(int id);
        Task UpdatePosition(Book book);
    }
}
