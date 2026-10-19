using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SuperShop.Data.Entities
{
    public class Order : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "Delivery Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime DeliveryDate { get; set; }

        [Required]
        public User User { get; set; }


        // é aqui que fazemos ligação entre Order e OrderDetail-> um pedido pode ter vários detalhes de pedido
        // it has a list of OrderDetail, and each OrderDetail has a reference to the Product that was ordered
        public IEnumerable<OrderDetail> Items { get; set; }

        // if no items, show 0, otherwise show the count of items in the order
        // readonly - get 
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Lines => Items == null ? 0 : Items.Count();

        // if Items is null, return 0, otherwise return the sum of the Quantity of each OrderDetail in Items
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Quantity => Items == null ? 0 : Items.Sum(i => i.Quantity);


        // return the sum of the Value of each OrderDetail in Items, if Items is null, return 0
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal Value => Items == null ? 0 : Items.Sum(i => i.Value);


        // return the OrderDate in local time, if OrderDate is null, return null
        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime? OrderDateLocal => this.OrderDate == null ? null : this.OrderDate.ToLocalTime();
    }
}
