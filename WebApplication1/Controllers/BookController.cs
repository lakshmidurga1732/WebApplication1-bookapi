using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApplication1.DTO;
using WebApplication1.Entity;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBook _bookService;
        private readonly IMapper _mapper;

        public BooksController(IBook bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<BookDTO>> GetAllBooks()
        {
            var books = _bookService.GetAllBooks();
            return Ok(_mapper.Map<IEnumerable<BookDTO>>(books));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<BookDTO> GetBookById(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return _mapper.Map<BookDTO>(book);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddBook(BookDTO bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            _bookService.AddBook(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.BookId }, bookDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateBook(int id, BookDTO bookDto)
        {
            if (id != bookDto.BookId)
            {
                return BadRequest();
            }

            var existingBook = _bookService.GetBookById(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            var updatedBook = _mapper.Map<Book>(bookDto);
            _bookService.UpdateBook(id, updatedBook);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteBook(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            _bookService.DeleteBook(id);
            return NoContent();
        }

        [HttpGet("searchByAuthor")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<BookDTO>> SearchByAuthor(string authorName)
        {
            var books = _bookService.SearchByAuthor(authorName);
            return Ok(_mapper.Map<IEnumerable<BookDTO>>(books));
        }

        [HttpGet("searchByBookName")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<BookDTO>> SearchByBookName(string bookName)
        {
            var books = _bookService.SearchByBookName(bookName);
            return Ok(_mapper.Map<IEnumerable<BookDTO>>(books));
        }

        [HttpGet("generalSearch")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<BookDTO>> GeneralSearch(string keyword)
        {
            var books = _bookService.GeneralSearch(keyword);
            return Ok(_mapper.Map<IEnumerable<BookDTO>>(books));
        }
    }
}
