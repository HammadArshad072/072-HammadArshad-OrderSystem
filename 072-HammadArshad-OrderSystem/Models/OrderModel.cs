using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _072_HammadArshad_OrderSystem.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name Is Required"),DataType(DataType.Text)]
        [StringLength(20,MinimumLength =5)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description Is Required")]
        [DataType(DataType.MultilineText),StringLength(500,MinimumLength =10)]
        public string Description { get; set; }
        [Required, Range(0,int.MaxValue)]
        public int Quantity { get; set; }
        [Required,DataType(DataType.Currency)]
        public Decimal Price { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string ImageSource { get; set; }
        public string Total { get; set; }
    }
}
