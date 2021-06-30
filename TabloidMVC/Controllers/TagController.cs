using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TabloidMVC.Models;
using TabloidMVC.Repositories;



namespace TabloidMVC.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepository;
        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        [Authorize(Roles = "Admin") ]
        public IActionResult Index()
        {
            List<Tag> tags = _tagRepository.GetAll();
            return View(tags);
        }
        public ActionResult Delete(int id)
        {
            Tag vm = _tagRepository.GetTagById(id);
            return View(vm);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Tag vm = _tagRepository.GetTagById(id);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tag tagUpdate, IFormCollection collection)
        {
            try
            {
                _tagRepository.Edit(tagUpdate);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _tagRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tag vm)
        {
            try
            {
                int retVal = _tagRepository.Add(vm);

                return RedirectToAction("Index");
            }
            catch
            {
                List<Tag> errTrap = _tagRepository.GetAll();
                return View(errTrap);
            }
        }


    }//End Class

}// End Namespace
