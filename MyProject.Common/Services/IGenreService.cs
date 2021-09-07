using MyProject.Common.Models.ClientModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Common.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreViewModel>> Get(int id);
        Task<GenreViewModel> GetById(int id);
    }
}
