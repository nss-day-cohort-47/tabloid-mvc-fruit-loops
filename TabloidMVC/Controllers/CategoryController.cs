using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public ActionResult Create(IFormCollection collection)
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
            return View();
        }

        // POST: CatergoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
