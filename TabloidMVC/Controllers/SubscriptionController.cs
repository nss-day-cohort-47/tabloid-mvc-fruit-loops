using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{


    public class SubscriptionController : Controller
    {

        private readonly ISubscriptionRepository _subscriptionRepository;
        //private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionController(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
           
        }


        // GET: SubscriptionController1
        public ActionResult SubscribedHome()
        {
            int loggedInUserId = GetCurrentUserProfileId();
            var vm =_subscriptionRepository.GetAllSubscribersPostsByUserId(loggedInUserId);
            return View(vm);
        }

        // GET: SubscriptionController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SubscriptionController1/Create
        public ActionResult Create(int Id)
        {
            int loggedInUserId = GetCurrentUserProfileId();
            _subscriptionRepository.AddSubscription(loggedInUserId, Id);
            return RedirectToAction("Details", "Post", new { id = Id });
        }

        public ActionResult Delete(int Id)
        {
            int loggedInUserId = GetCurrentUserProfileId();
            _subscriptionRepository.RemoveSubscription(loggedInUserId, Id);
            return RedirectToAction("Details", "Post", new { id = Id });
        }

        // POST: SubscriptionController1/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Post post)
        //{
        //    int loggedInUserId = GetCurrentUserProfileId();

        //    try
        //    {
        //        _subscriptionRepository.AddSubscription(loggedInUserId, post.UserProfileId);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: SubscriptionController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SubscriptionController1/Edit/5
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

        // GET: SubscriptionController1/Delete/5
       

        // POST: SubscriptionController1/Delete/5
        

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(id != null)
            {
                return int.Parse(id);
            }
            return 0;
        }

    }
}
