using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public int Post { get; set; }
        public int UserProfileId {get; set;}
        public string Subject { get; set; }
        public string Conent { get; set; }
        public DateTime CreateDateTime { get; set; }

    }
}
