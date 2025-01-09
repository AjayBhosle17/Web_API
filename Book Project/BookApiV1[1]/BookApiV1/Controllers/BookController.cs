using BookApi.Models;
using BookApi.Models.BAL;
using BookApiV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace BookApi.Controllers
{
    public class BookController : ApiController
    {
        IBookBL _bl;

        public BookController(IBookBL bl)
        {
            _bl = bl;
        }

        [HttpGet]
        [ResponseType(typeof(List<Book>))]
        public IHttpActionResult GetAll() //*: Retrieve a list of all books.
        {
            try
            {
                var book = _bl.GetAll();

                if (book != null && book.Count() > 0)
                {
                    return Ok(book);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);  //500
            }
        }

        [HttpGet]
        [ResponseType(typeof(List<Book>))]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    Book book = _bl.GetById(id);
                    if (book != null)
                    {
                        return Ok(book); //200
                    }
                    else
                    {
                        return NotFound(); //404
                    }
                }
                else
                {
                    return BadRequest("BookID can not be negative"); //400
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); //500
            }
        }

        [HttpPost]
        public IHttpActionResult Create(Book book)
        {
            try
            {
                if (book != null)
                {
                    _bl.Create(book);
                    return Ok();
                }
                else
                {
                    return BadRequest();  //400
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); //500
            }
        }

        [HttpPut]
        public IHttpActionResult Update(int id,Book book)
        {
            try
            {
                if (id > 0 && book != null)
                {
                    if (id == book.ID)
                    {
                        _bl.Update(book);
                        return Ok();   //200
                    }
                    else
                    {
                        return NotFound(); // 404 
                    }
                }
                else
                {
                    return BadRequest(); //400
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); //500 
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    Book book = _bl.GetById(id);
                    if (book != null)
                    {
                        _bl.Delete(id);
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest(); //400
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); //500
            }
        }
    
    
    }
}
