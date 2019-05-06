using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrainingApi.Data;


namespace TrainingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository _Repository;
        private ILogger<Category> _logger;

        public CategoryController(IRepository repository, ILogger<Category> logger)
        {
            _logger = logger;
            _Repository = repository;
        }

        // GET api/category
        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            var Categorys = _Repository.GetCategories(_logger);
            return Ok(Categorys);
        }

        // GET api/category/5
        [HttpGet("{id}")]
        public ActionResult<Category> Get(int id)
        {
            var category = _Repository.GetCategoryById(id, _logger);
            return Ok(category);
        }

        // POST api/category
        [HttpPost]
        public ActionResult<Category> Post([FromBody] Category newCategory)
        {
            var postedCategory = _Repository.PostNewCategory(newCategory, _logger);
            return Ok(postedCategory);
        }

        // PUT api/category/5
        [HttpPut("{id}")]
        public ActionResult<Category> Put(int id, [FromBody] Category updateCategory)
        {
            var  postedCategory = _Repository.UpdateCategory(id, updateCategory, _logger);
            return Ok(postedCategory);
        }

        /* // DELETE api/category/5
         [HttpDelete("{id}")]
         public void Delete(int id)
         {
         }*/
    }
}
