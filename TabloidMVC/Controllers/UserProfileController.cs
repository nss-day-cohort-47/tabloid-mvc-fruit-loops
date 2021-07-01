using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
        public ActionResult UserType(int id)
        {
            var user = _userProfileRepository.GetById(id);
            if (user == null)
            {
                return NoContent();
            }
            return View(user);
        }

        // POST: UserProfile/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserType(int id, UserProfile user)
        {
            var userdata = _userProfileRepository.GetById(id);
            if (userdata.UserTypeId == 1)
            {

                try
                {
                    _userProfileRepository.MakeAuthor(id);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(user);
                }
            }
            else
            {
                try
                {
                    _userProfileRepository.MakeAdmin(id);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(user);
                }
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

            if (user.UserTypeId == 1)
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
                    return RedirectToAction("Warning");
                }
            }
            else
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
        }

        public IActionResult Warning()
        {
            return View();
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
