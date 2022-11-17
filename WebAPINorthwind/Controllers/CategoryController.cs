using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using WebAPINorthwind.Models;

namespace WebAPINorthwind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly NorthwindContext context;

        public CategoryController(NorthwindContext context)
        {
            this.context = context;
        }

        //GET /api/Category/
        [HttpGet]
        public dynamic Get()
        {
            dynamic categories = (from c in context.Categories select c.CategoryName).ToList();
            return categories;

        }

        //GET /api/Category/id
        [HttpGet("{id}")]
        public dynamic Get(int id)
        {
            dynamic category = (from c in context.Categories where c.CategoryId == id select c.CategoryName).SingleOrDefault();
            return category;
        }
    }
}
