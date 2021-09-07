using AutoMapper;
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
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreViewModel>> Get(int id)
        {
            var query = await _genreRepository.GetOdered(id);

            var model = _mapper.Map<IEnumerable<GenreViewModel>>(query);


            return model;
        }

        public async Task<GenreViewModel> GetById(int id)
        {
            var entity = await _genreRepository.GetById(id);

            var model = _mapper.Map<GenreViewModel>(entity);


            return model;

        }
    }
}
