using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;


namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAll();
        int Add(Tag add);

        void Delete(int id);
        Tag GetTagById(int id);

        void Edit(Tag toEdit);
    }
}
