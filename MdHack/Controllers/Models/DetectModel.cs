using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MdHack.Controllers.Models
{
    public class DetectModel
    {
        public IFormFile Face { get; set; }
    }
}
