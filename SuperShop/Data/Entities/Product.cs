using System;
using System.ComponentModel.DataAnnotations;

namespace SuperShop.Data.Entities
{
    // after making changes here (to DB), must make respective migration
    // go to Package Manager Console "add-migration ModifyProducts" and then "update-database"

    // classe que representa a tabela de produtos na base de dados
    public class Product : IEntity
    {

        // senao chamassemos "Id" teria que usar dataAnnotations [Key]
        // automaticamente metido como chave primaria  
        // porque o nome da propiedade Id e é inteiro
        public int Id { get; set; }

        // the errormessage doesnt make sense because it's not possible to insert more than 50 char
        [Required] //data annotation that makes name required
        [MaxLength(50, ErrorMessage = "The field {0} cannot contain more than {1} characters.")]
        public string Name { get; set; }


        //DataAnnotations
        // vai formatar o preço para moeda com 2 casas decimais, mas quando for editado
        // deixa o usar o escrever o preço como quiser, sem formatar
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        // o link da imagem; cada produto vai ter uma imagem
        // Display: para aparecer "Image" na página web em vez de "ImageUrl"
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        // Display: para aparecer "Last Purchase" na página web em vez de "LastPurchase"
        [Display(Name = "Last Purchase")]
        public DateTime? LastPurchase { get; set; }

        // Display: para aparecer "Last Purchase" na página web em vez de "LastPurchase"
        [Display(Name = "Last Sale")]
        public DateTime? LastSale { get; set; } // '?' makes datetime optional

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }

        //DataAnnotations
        // vai formatar o stock para numero com 2 casas decimais, mas quando for editado
        // deixa o usar o escrever o stock como quiser, sem formatar
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Stock { get; set; }

        public User User { get; set; }


        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImageUrl))
                {
                    return "http://supershop2026.somee.com/images/noimage.jfif";
                }

                return $"http://supershop2026.somee.com{ImageUrl.Substring(1)}";
            }
        }
    }
}
