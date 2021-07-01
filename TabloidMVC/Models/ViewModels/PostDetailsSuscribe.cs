using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class PostDetailsSuscribe
    {
        public Post Post { get; set; }
        public bool IsSubscribed { get; set; }
    }
}
