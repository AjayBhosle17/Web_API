using BookApiV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApi.Models.BAL
{
    public interface IBookBL
    {
        List<Book> GetAll();
        Book GetById(int id);
        void Create(Book book);
        void Update(Book book); 
        void Delete(int id); 
    }
}
