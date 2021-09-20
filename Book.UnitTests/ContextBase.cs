using Microsoft.EntityFrameworkCore;
using MyProject.Dal.SqlContext;
using System;

namespace Book.UnitTests
{
    public abstract class ContextBase
    {
        protected MyProjectContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<MyProjectContext>()
                                 .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                 .Options;
            return new MyProjectContext(options);
        }
    }
}
