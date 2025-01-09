using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BookApiV1.Models;

namespace BookApi.Models.Repositories
{
       
    public class BookRepositories : IBookRepositories
    {
        ApiTaskEntities _apiTE;

        public BookRepositories(ApiTaskEntities apiTE)
        {
            _apiTE = apiTE;
        }

        public void Create(Book book)
        {
            _apiTE.Books.Add(book);
            _apiTE.SaveChanges();
        }

        public void Delete(int id)
        {
            Book book = _apiTE.Books.Find(id);
            _apiTE.Books.Remove(book);
            _apiTE.SaveChanges();
        }

        public List<Book> GetAll()
        {
            List<Book> books = _apiTE.Books.ToList();
            return books;
        }

        public Book GetById(int id)
        {
            Book book = _apiTE.Books.Find(id);
            return book;
        }

        public void Update(Book book)
        {
            _apiTE.Books.Attach(book);
            _apiTE.Entry(book).State = EntityState.Modified;
            _apiTE.SaveChanges();
        }
    }
}