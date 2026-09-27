using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using SuperShop.Data.Entities;

namespace SuperShop.Models
{
    public class ProductViewModel : Product
    {
        // to handle file upload
        [Display(Name = " Image")]
        public IFormFile ImageFile { get; set; }
    }
}
