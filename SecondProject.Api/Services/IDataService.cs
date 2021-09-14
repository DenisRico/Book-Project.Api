using SecondProject.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondProject.Api.Services
{
    public interface IDataService
    {
        Task<IEnumerable<BookApiModel>> GetBooksAsync(string auth);
    }
}
