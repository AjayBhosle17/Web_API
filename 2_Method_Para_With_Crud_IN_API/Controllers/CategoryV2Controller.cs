/*using _2_Method_Para_With_Crud_IN_API.Models;
using _2_Method_Para_With_Crud_IN_API.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// we can achieve all request/ method using post but it violate srp principle


namespace _2_Method_Para_With_Crud_IN_API.Controllers
{
    public class CategoryV2Controller : ApiController
    {
        ProductDBContext _dbContext = new ProductDBContext();

        [HttpGet]

        public IHttpActionResult GetAll()
        {
            var category = _dbContext.Categories.ToList();

            return Ok(category);
        }


        [HttpGet]
        public IHttpActionResult GetById([FromUri] int? id)
        {

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


        // manage api for create/update/delete category

        public IHttpActionResult Manage(Category category)
        {


            if (category.IsCreate && category.IsUpdate && category.IsDelete)
            {
                return BadRequest("Create / update/delete all operation not allowed same time");

            }
            else if (!category.IsCreate && !category.IsUpdate && !category.IsDelete)
            {

                return BadRequest("at least Create/ update / delete  opertion mandatory at a time");

            }
            else if (category.IsCreate && category.IsUpdate && !category.IsDelete)
            {
                return BadRequest("create / update all operation not allowed at a same time");

            }
            else if (category.IsCreate && !category.IsUpdate && category.IsDelete)
            {
                return BadRequest("create / delete all operation not allowed at same time");
            }
            else if (!category.IsCreate && category.IsUpdate && category.IsDelete)
            {
                return BadRequest("Update / delete all operation mpt allowed at same time");
            }
            else if (category.IsCreate && !category.IsUpdate && !category.IsDelete)
            {

                //  create operation logic

                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();


                return Ok("Category created successfully.");

            }
            else if (!category.IsCreate && category.IsUpdate && !category.IsDelete)
            {
                //  update logic

                var exitCatgeory = _dbContext.Categories.Find(category.Id);

                exitCatgeory.Name = category.Name;
                exitCatgeory.Rating = category.Rating;

                _dbContext.SaveChanges();

                return Ok("Category updated successfully.");
            }
            else
            {
                //delete logic

                var exitCategory = _dbContext.Categories.Find(category.Id);

                _dbContext.Categories.Remove(exitCategory);
                _dbContext.SaveChanges();
                return Ok("Category deleted successfully.");
            }
        }

    }
}
*/