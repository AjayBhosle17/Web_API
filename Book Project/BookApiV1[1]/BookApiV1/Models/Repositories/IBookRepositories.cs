using BookApiV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookApi.Models.Repositories
{
    public interface IBookRepositories
    {
        List<Book> GetAll();
        Book GetById(int id);
        void Create(Book book);
        void Update(Book book); 
        void Delete(int id);
    }
}