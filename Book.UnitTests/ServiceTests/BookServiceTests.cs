using MyProject.Dal.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Book.UnitTests.ServiceTests
{
    public class BookServiceTests : ContextBase
    {
        [Fact]
        public async Task List_should_return_list_of_books()
        {
            // Arrange
            using var dbContext = GetDbContext();

            await dbContext.SaveChangesAsync();

            // Act
            var service = new BookRepository(dbContext);
            var result = await service.GetOdered();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count() > 0);
        }
    }
}
