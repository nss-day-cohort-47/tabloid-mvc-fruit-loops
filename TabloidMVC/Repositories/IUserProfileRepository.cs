using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetByEmail(string email);

        public UserProfile Add(UserProfile user);

        List<UserProfile> GetAllUserProfiles();
        public UserProfile GetById(int id);

        public void Delete(int id);

        public void Reactivate(int id);

        public int CheckforAdmin();

        public void MakeAdmin(int id);
        public void MakeAuthor(int id);
    }
}