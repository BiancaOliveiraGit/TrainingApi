using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TrainingApi.ErrorMiddleware;

namespace TrainingApi.Data
{
    public partial class Repository 
    {        

        public Category GetCategoryById(int id, ILogger<Category> logger)
        {
            Category item = new Category();
            try
            {
                item = _appDbContext.Categories.Where(w => w.CategoryId == id)
                                        .Select(s => s).FirstOrDefault();
                return item;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in GetCategoryById: {id}");
            }
            return item;
        }

        public IEnumerable<Category> GetCategories(ILogger<Category> logger)
        {
            List<Category> list = new List<Category>();
            try
            {
                list = _appDbContext.Categories.Select(s => s).ToList();
                return list;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in GetCategories");
            }
            return list;
        }

        public Category PostNewCategory(Category newCategory, ILogger<Category> logger)
        {
            try
            {
                //check that Category doesn't exist
                var exists = _appDbContext.Categories.Where(w => w.Name == newCategory.Name)
                                                  .Select(s => s).FirstOrDefault();
                if (exists != null)
                    throw new HttpStatusCodeException(HttpStatusCode.BadRequest, string.Format("Category {0} already exists", newCategory.Name));

                var item = _appDbContext.Add(newCategory);
                item.State = Microsoft.EntityFrameworkCore.EntityState.Added;
                var isOk = _appDbContext.SaveChanges();

                return item.Entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in PostNewCategory: {newCategory.Name}");
                throw e;
            }
        }

        public Category UpdateCategory(int id, Category updateCategory, ILogger<Category> logger)
        {
            try
            {
                //check that Category exists
                var existingCategory = _appDbContext.Categories.Where(w => w.CategoryId == updateCategory.CategoryId)
                                                  .Select(s => s).FirstOrDefault();
                if (existingCategory != null)
                    throw new HttpStatusCodeException(HttpStatusCode.BadRequest, string.Format("CategoryID {0},- {1} Doesn't Exist in system", updateCategory.CategoryId, updateCategory.Name));

                //update Category
                existingCategory.Name = updateCategory.Name;
     
                var isOk = _appDbContext.SaveChanges();

                return existingCategory;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in UpdateCategory: {updateCategory.CategoryId} - {updateCategory.Name}");
            }
            return updateCategory;
        }
    }
}
