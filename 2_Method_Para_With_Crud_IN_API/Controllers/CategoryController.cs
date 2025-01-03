using _2_Method_Para_With_Crud_IN_API.Models;
using _2_Method_Para_With_Crud_IN_API.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _2_Method_Para_With_Crud_IN_API.Controllers
{
    public class CategoryController : ApiController
    {
        ProductDBContext _dbContext= new ProductDBContext();

        [HttpGet]

        public IHttpActionResult GetAll()
        {
           var category = _dbContext.Categories.ToList();

            return Ok(category);
        }


        [HttpGet]
        public IHttpActionResult GetById([FromUri]int? id) {

            if (id > 0)
            {

                Category category = _dbContext.Categories.Find(id);

                if (category != null)
                {
                    return Ok(category); //200
                }
                else
                {
                    return NotFound();   // 404
                }

            }
                
            return BadRequest("Category Id Should be Grater Than 0");
            

        }

        [HttpPost]
        public IHttpActionResult Create([FromBody] Category category) {

            if (category != null)
            {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();

                return Created("DefaultApi", category);

            }

                return BadRequest();   //400
            
        }


        [HttpPut]   // update

        public IHttpActionResult Update([FromUri]int? id , [FromBody]Category category)
        {
            if (id > 0)
            {
                if (id == category.Id) {

                    Category dbCategory = _dbContext.Categories.Find(id);

                    if (dbCategory != null)
                    {
                        dbCategory.Name = category.Name;
                        dbCategory.Rating = category.Rating;

                        _dbContext.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                   
                }
                return BadRequest();
                
            }
            return BadRequest("CAtegory Id Should be Grater Than 0");
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int? id) {

            if (id > 0) {

                Category category = _dbContext.Categories.Find(id);

                if (category != null) { 
                
                    _dbContext.Categories.Remove(category);
                    _dbContext.SaveChanges();
                    return Ok();    
                }
                return NotFound();
            }
            return BadRequest();
        }



    }
}
