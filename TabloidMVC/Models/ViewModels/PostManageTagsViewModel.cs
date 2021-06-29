using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class PostManageTagsViewModel
    {
        public int PostId { get; set; }

        public int TagId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsApproved { get; set; }

        public List<Tag> PostTags { get; set; }
    }
}
