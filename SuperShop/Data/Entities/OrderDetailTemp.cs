using System.ComponentModel.DataAnnotations;

namespace SuperShop.Data.Entities
{
    public class OrderDetailTemp : IEntity
    {
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Product Product { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public int Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value => Price * (decimal)Quantity;
    }
}
