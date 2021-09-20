using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Common.Models.ClientModel;
using MyProject.Common.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BookController : MPControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        public BookController()
        {
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BookViewModel>))]
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var info = GetAuthInfo();
            try
            {
                return Ok(await _service.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BookViewModel book)
        {
            try
            {

                if (book.Name == null) return BadRequest("book name is required");

                await _service.AddBook(book);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        /// <summary>
        /// Обновление Book
        /// </summary>
        /// <param name="Book"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdatePosition([FromBody] BookViewModel book)
        {
            try
            {
                if (book == null) return BadRequest("book is required");

                await _service.UpdateBook(book);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = await _service.Delete(id);

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookViewModel))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await _service.GetById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
