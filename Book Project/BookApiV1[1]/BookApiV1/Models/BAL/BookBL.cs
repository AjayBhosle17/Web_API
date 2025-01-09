using BookApi.Models.Repositories;
using BookApiV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookApi.Models.BAL
{
    public class BookBL : IBookBL
    {
        IBookRepositories _bRepo;

        public BookBL(IBookRepositories bRepo)
        {
            _bRepo = bRepo;
        }

        public void Create(Book book)
        {
            _bRepo.Create(book);
        }

        public void Delete(int id)
        {
            _bRepo.Delete(id);
        }

        public List<Book> GetAll()
        {
            return _bRepo.GetAll();
        }

        public Book GetById(int id)
        {
            return _bRepo.GetById(id);
        }

        public void Update(Book book)
        {
            _bRepo.Update(book);
        }
    }
}