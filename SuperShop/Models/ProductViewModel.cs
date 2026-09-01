using Microsoft.AspNetCore.Http;
using SuperShop.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace SuperShop.Models
{
    public class ProductViewModel : Product
    {
        // to handle file upload
        [Display(Name = " Image")]
        public IFormFile ImageFile { get; set; }
    }
}
