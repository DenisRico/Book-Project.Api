using MyProject.Common.Models;
using MyProject.Common.Models.ClientModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Common.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookViewModel>> GetAll();
        Task<BookViewModel> GetById(int id);
        Task AddBook(BookViewModel book);
        Task<Book> Delete(int id);
        Task UpdateBook(BookViewModel book);
    }
}
