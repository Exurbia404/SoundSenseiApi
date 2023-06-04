using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string BrandImageLink { get; set; }
    }
}
