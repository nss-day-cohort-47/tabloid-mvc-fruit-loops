using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        // GET: CommentsController
        private readonly ICommentsRepository _commentsRepository;
        private readonly IPostRepository _postRepository;
        public CommentsController(ICommentsRepository commentsRepository, IPostRepository postRepository)
        {
            _commentsRepository = commentsRepository;
            _postRepository = postRepository;
        }
        public ActionResult Index(int id)
        {
            PostCommentsViewModel vm = new PostCommentsViewModel()
            {
                Comments = _commentsRepository.GetAllPostCommentsById(id),
                Post = new Post() {
                    Id = id,
                }

            };

            return View(vm);

        }

        // GET: CommentsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CommentController/Create
        public ActionResult Create(int id)
        {


            Post posts = _postRepository.GetPublishedPostById(id, GetCurrentUserProfileId());



            PostCommentsViewModel vm = new PostCommentsViewModel()
            {
                comment = new Comments(),
                Post = posts,
            };
            vm.comment.PostId = id;

            return View(vm);
        }

        // POST: CommentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comments Comment, Post Posts)
        {

                Comment.UserProfileId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                Comment.PostId = Posts.Id;
                _commentsRepository.AddComment(Comment);
                return RedirectToAction("Index", new { id = Comment.PostId });

        }

        // GET: CommentsController/Edit/5
        public ActionResult Edit(int id)
        {
            Comments comments = _commentsRepository.GetCommentById(id);
            
            if (comments == null)
            {
                return NotFound();
            }
            if (GetCurrentUserProfileId() == comments.UserProfileId || User.IsInRole("Admin"))
            {
                return View(comments);
            }
            return NotFound();
        }

        // POST: CommentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Comments comments)
        {
            try
            {
                _commentsRepository.Update(comments);
                return RedirectToAction("Index", "Comments", new { id });
            }
            catch
            {
                return View(comments);
            }
        }

        // GET: CommentsController/Delete/5
        public ActionResult Delete(int id)
        {
            Comments comments = _commentsRepository.GetCommentById(id);
            return View(comments);
        }

        // POST: CommentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Comments comments)
        {
            try
            {
                _commentsRepository.DeleteComment(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(comments);
            }
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
