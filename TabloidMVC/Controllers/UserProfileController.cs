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
    public class UserProfileController : Controller
    {
        private readonly IUserProfileRepository _userProfileRepository;
        //private readonly ICategoryRepository _categoryRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
            //_categoryRepository = categoryRepository;
        }


        // GET: UserProfile
        public ActionResult Index()
        {
            var userProfiles = _userProfileRepository.GetAllUserProfiles();
            return View(userProfiles);
        }

        // GET: UserProfile/Details/5
        public ActionResult Details(int id)
        {
            var userProfiles = _userProfileRepository.GetById(id);
            return View(userProfiles);
        }

        // GET: UserProfile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserProfile/Create
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

        // GET: UserProfile/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserProfile/Edit/5
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

        // GET: UserProfile/Delete/5
        public IActionResult Deactivate(int id)
        {
            var user = _userProfileRepository.GetById(id);
            return View(user);
        }

        // POST: DogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate(int id, UserProfile user)
        {
            int admin = _userProfileRepository.CheckforAdmin();

            if (admin > 1)
            {

                try
                {
                    _userProfileRepository.Delete(id);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(user);
                }
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>alert('You cannot delete the only admin!');</script>");
            }
        }

        public IActionResult Reactivate(int id)
        {
            var user = _userProfileRepository.GetById(id);
            return View(user);
        }

        // POST: DogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reactivate(int id, UserProfile user)
        {
            try
            {
                _userProfileRepository.Reactivate(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(user);
            }
        }
    }
}
