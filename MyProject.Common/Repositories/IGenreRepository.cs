using MyProject.Common.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Common.Repositories
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetOdered(int id);
        Task<Genre> GetById(int id);
    }
}
