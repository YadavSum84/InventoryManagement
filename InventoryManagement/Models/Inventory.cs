using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace InventoryAPI.Models
{
    [Table("Inventory")]
    public class Inventory
    {
        [Key]
        public int ItemId { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Required.")]
        public Decimal? Price { get; set; }
        [Required(ErrorMessage = "Required.")]
        public int? Unit { get; set; }
    }
    
    
}