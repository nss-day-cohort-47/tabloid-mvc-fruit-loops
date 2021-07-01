using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models
{
    public class Image
    {
       
        public int ImageId { get; set; }

       
        public string Title { get; set; }

        public string ImageName { get; set; }

        public IFormFile ImageFile { get; set; }

        public int userid { get; set; }
    }
}
