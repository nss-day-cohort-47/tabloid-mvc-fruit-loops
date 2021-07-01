using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICommentsRepository _commentsRepository;
        private readonly ITagRepository _tagRepository;

        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository, ICommentsRepository commentsRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _commentsRepository = commentsRepository;
            _tagRepository = tagRepository;
        }

        public IActionResult Index()
        {
            var posts = _postRepository.GetAllPublishedPosts();
            return View(posts);
        }

        public IActionResult MyPost()
        {
            int userId = GetCurrentUserProfileId();
            var posts = _postRepository.GetAllPostsByUser(userId);
            return View(posts);
        }

        public IActionResult Details(int id)
        {
            var post = _postRepository.GetPublishedPostById(id, GetCurrentUserProfileId());
            if (post == null)
            {
                int userId = GetCurrentUserProfileId();
                post = _postRepository.GetUserPostById(id, userId);
                if (post == null)
                {
                    return NotFound();
                }
            }
            return View(post);
        }

        public ActionResult Delete(int id)
        {
            int userId = GetCurrentUserProfileId();
            var post = _postRepository.GetPublishedPostById(id, GetCurrentUserProfileId());

            if (userId == post.UserProfileId || User.IsInRole("Admin"))
            {
                return View(post);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: DogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Post post)
        {
            try
            {
                _postRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(post);
            }
        }

        public IActionResult Create()
        {
            var vm = new PostCreateViewModel();
            vm.CategoryOptions = _categoryRepository.GetAllCategories();
            return View(vm);
        }

        public IActionResult ManageTags(int id)
        {
            PostManageTagsViewModel vm = _postRepository.GetUserPostByIdAndTags(id);
            vm.PostTags = _tagRepository.GetAllByPost(id);
            return View(vm);
        }

        public IActionResult AddPostTag(int postId)
        {
            List<Tag> tags = _tagRepository.GetAll();
            PostManageTagsViewModel vm = new();
            vm.PostId = postId;
            vm.PostTags = tags;
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddPostTag(PostManageTagsViewModel vm)
        {
            try
            {
                _tagRepository.AddPostTag(vm.TagId, vm.PostId);
                return RedirectToAction("ManageTags", new { id = vm.PostId});
            }
            catch
            {
                return RedirectToAction("ManageTags", new { id = vm.PostId });
            }
        }
        public IActionResult DeletePostTag(int Id, int PostId)
        {
            _tagRepository.DeletePostTag(Id, PostId);
            return RedirectToAction("ManageTags", new { id = PostId });
        }

        [HttpPost]
        public IActionResult Create(PostCreateViewModel vm)
        {
            try
            {
                vm.Post.CreateDateTime = DateAndTime.Now;
                vm.Post.IsApproved = true;
                vm.Post.UserProfileId = GetCurrentUserProfileId();

                _postRepository.Add(vm.Post);

                return RedirectToAction("Details", new { id = vm.Post.Id });
            }
            catch
            {
                vm.CategoryOptions = _categoryRepository.GetAllCategories();
                return View(vm);
            }
        }

        // GET: OwnerController/Edit/5
        public IActionResult Edit(int id)
        {
            var post = _postRepository.GetPublishedPostById(id, GetCurrentUserProfileId());

            if (post == null)
            {
                return NoContent();
            }

            var vm = new PostCreateViewModel();
            vm.Post = post;
            vm.CategoryOptions = _categoryRepository.GetAllCategories();
            if (GetCurrentUserProfileId() == post.UserProfileId || User.IsInRole("Admin"))
            {
                return View(vm);
            }
            else
            {
                return NoContent();
            }
                
        }
        // POST: OwnerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(int id, PostCreateViewModel vm)
        {
            try
            {
                _postRepository.Update(vm.Post);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }


        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }

    }
}
