using System.ComponentModel.DataAnnotations;

namespace api.DTO
{
    public class OrderDTO
    {
        [Required]
        public string BasketId { get; set; }
        [Required]
        public AddressDTO ShipToAddress { get; set; }
    }
}