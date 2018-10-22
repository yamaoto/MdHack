using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MdHack.Controllers.Models
{
    public class AddFaceModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Passport { get; set; }
        [Required]
        public IFormFile Main { get; set; }
        [Required]
        public IFormFile One { get; set; }
        [Required]
        public IFormFile Two { get; set; }
    }
}