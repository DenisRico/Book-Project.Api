using Microsoft.AspNetCore.Mvc;
using MyProject.Api.Controllers;
using MyProject.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Book.UnitTests.ControllerTests
{
    public class BookControllerTest
    {
        [Fact]
        public void Index_should_return_public_view_for_anonymous_user()
        {
            var controller = new BookController().WithAnonymousIdentity();

            var result = controller.GetBooks() as Task<IActionResult>;

            Assert.NotNull(result);
        }
    }
}
