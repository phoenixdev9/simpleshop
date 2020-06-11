using System;
using System.ComponentModel.DataAnnotations;

namespace api.DTO
{
    public class BasketDTO
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Must be bigger than 0")]
        public int Quantity { get; set; }
    }
}