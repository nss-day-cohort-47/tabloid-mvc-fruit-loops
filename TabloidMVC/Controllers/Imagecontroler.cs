using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class Imagecontroler : Controller
    {

        private readonly IUserProfileRepository _userRepo;

        public Imagecontroler(IUserProfileRepository userProfileRepository)
        {
            _userRepo = userProfileRepository;
        }
        // GET: Imagecontroler
        public ActionResult Index()
        {
            return View();
        }

        // GET: Imagecontroler/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Imagecontroler/Create
        public ActionResult Create(int id)
        {
            ImageViewModel vm = new ImageViewModel()
            {
                Loggedinuser = id
            };
            return View(vm);
        }

        // POST: Imagecontroler/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ImageViewModel imageModel)
        {
            if (ModelState.IsValid)
            {
                
                //Save image to wwwroot/image
                string wwwRootPath = @"C:\Users\frank\workspace\csharp\tabloid-mvc-fruit-loops\TabloidMVC\wwwroot\uploads\";
                string fileName = Path.GetFileNameWithoutExtension(imageModel.Image.ImageFile.FileName);
                string extension = Path.GetExtension(imageModel.Image.ImageFile.FileName);
                imageModel.Image.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath, fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await imageModel.Image.ImageFile.CopyToAsync(fileStream);
                }
                string root = @"~/uploads/";
                string rootpath = Path.Combine(root, fileName);
                _userRepo.AddImage(imageModel.Loggedinuser, rootpath);
                return RedirectToAction( "Index", "UserProfile");
            }

            return View(imageModel);
        }

        // GET: Imagecontroler/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Imagecontroler/Edit/5
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

        // GET: Imagecontroler/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Imagecontroler/Delete/5
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
