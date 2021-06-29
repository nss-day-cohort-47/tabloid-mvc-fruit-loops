using System.Collections.Generic;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;

namespace TabloidMVC.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        List<Post> GetAllPublishedPosts();
        Post GetPublishedPostById(int id, int currentUser);
        Post GetUserPostById(int id, int userProfileId);
        public List<Post> GetAllPostsByUser(int userProfileId);
        public void Delete(int id);
        public void Update(Post post);
        PostManageTagsViewModel GetUserPostByIdAndTags(int id);
    }
}