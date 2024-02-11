using WebApplication1.Entity;

namespace WebApplication1.Services
{
    public interface IBook
    {
        
            
            List<Book> GetAllBooks();

            
            Book GetBookById(int id);

            
            void AddBook(Book book);

            
            void UpdateBook(int id, Book book);

            
            void DeleteBook(int id);
        List<Book> SearchByAuthor(string authorName);

        List<Book> SearchByBookName(string bookName);

        List<Book> GeneralSearch(string keyword);

    }
}
