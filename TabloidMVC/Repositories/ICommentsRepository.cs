using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICommentsRepository
    {
        public void AddComment(Comments comment);
        public Comments GetCommentById(int id);
        public List<Comments> GetAllComments();
        public List<Comments> GetAllPostCommentsById(int id);
        public void Update(Comments comments);
        void DeleteComment(int commentId);

    }
}
