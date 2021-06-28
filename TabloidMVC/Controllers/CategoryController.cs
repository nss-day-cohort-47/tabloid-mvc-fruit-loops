using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(IPostRepository postRepository, ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
        }



        // GET: CatergoryController
        public ActionResult Index()
        {
            var categories = _categoryRepository.GetAllCategories();
            return View(categories);
        }

        // GET: CatergoryController/Details/5
        public ActionResult Details(int id)
        {

            //var category = _categoryRepository.GetCategoryById(id);
            //if (category == null)
            //{
            //    int userId = GetCurrentUserProfileId();
            //    category = _categoryRepository.GetCategoryById(id, userId);
            //    if (category == null)
            //    {
            //        return NotFound();
            //    }
            //}
            //return View(category);

            var categories = _categoryRepository.GetAllCategories();
            return View(categories);
        }

        // GET: CatergoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CatergoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                _categoryRepository.AddCategory(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(category);
            }
        }

        // GET: CatergoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CatergoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CatergoryController/Delete/5
        public ActionResult Delete(int id)
        {
            Category category = _categoryRepository.GetCategoryById(id);
            return View(category);
        }

        // POST: CatergoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                _categoryRepository.DeleteCategory(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(category);
            }
        }
    }
}
