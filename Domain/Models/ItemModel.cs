using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;
    public class ItemModel
    {
    public int Id { get; set; }

    [Display(Name = "Item Name")]
    [Required]
    public String? Name { get; set; }

    public String? Description { get; set; }
    [Range(0, 10000)]
    [Column(TypeName = "decimal(18, 2)")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

   public string? PictureURL { get; set; }
}

