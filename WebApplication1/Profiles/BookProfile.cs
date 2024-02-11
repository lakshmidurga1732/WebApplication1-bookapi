using AutoMapper;
using WebApplication1.DTO;
using WebApplication1.Entity;

namespace WebApplication1.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDTO>();
            CreateMap<BookDTO, Book>();
        }
    }
}
