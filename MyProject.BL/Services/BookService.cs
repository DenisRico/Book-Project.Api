using AutoMapper;
using MyProject.Common.Models;
using MyProject.Common.Models.ClientModel;
using MyProject.Common.Repositories;
using MyProject.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public BookService()
        {
        }

        public async Task<IEnumerable<BookViewModel>> GetAll()
        {
            var query = await _bookRepository.GetOdered();

            var model = _mapper.Map<IEnumerable<BookViewModel>>(query);


            return model;

        }

        public async Task AddBook(BookViewModel book)
        {
            var model = _mapper.Map<Book>(book);

            var arrayOfGenres = book.GenreIds;

            await _bookRepository.Add(model, arrayOfGenres);
        }



        public async Task<Book> Delete(int id)
        {
            var model = await _bookRepository.Delete(id);

            return model;
        }

        public async Task UpdateBook(BookViewModel book)
        {

            var model = _mapper.Map<Book>(book);

            await _bookRepository.UpdatePosition(model);
        }

        public async Task<BookViewModel> GetById(int id)
        {
            var entity = await _bookRepository.GetById(id);

            var model = _mapper.Map<BookViewModel>(entity);


            return model;

        }
    }
}
