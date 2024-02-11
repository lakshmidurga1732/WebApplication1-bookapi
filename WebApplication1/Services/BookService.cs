using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Entity;

namespace WebApplication1.Services
{
    public class BookService : IBook
    {
        private readonly MyContext _context;

        public BookService(MyContext context)
        {
            _context = context;
        }

        public void AddBook(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteBook(int id)
        {
            Book book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book GetBookById(int id)
        {
            return _context.Books.Find(id);
        }

        public void UpdateBook(int id, Book book)
        {
            var existingBook = _context.Books.Find(id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Genre = book.Genre;
                existingBook.ISBN = book.ISBN;
                existingBook.PublishDate = book.PublishDate;

                _context.SaveChanges();
            }
        }

        public List<Book> SearchByAuthor(string authorName)
        {
            return _context.Books.Where(b => b.Author.Contains(authorName)).ToList();
        }

        public List<Book> SearchByBookName(string bookName)
        {
            return _context.Books.Where(b => b.Title.Contains(bookName)).ToList();
        }

        public List<Book> GeneralSearch(string keyword)
        {
            return _context.Books.Where(b =>
                b.Author.Contains(keyword) ||
                b.Title.Contains(keyword) ||
                b.Genre.Contains(keyword) ||
                b.ISBN.Contains(keyword)).ToList();
        }
    }
}
