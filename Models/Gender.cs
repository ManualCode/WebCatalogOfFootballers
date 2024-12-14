using System.ComponentModel.DataAnnotations;

namespace WebCatalogOfFootballers.Models
{
    public class Gender
    {
        [Key]
        public int Id { get; set; }

        public string? GenderName { get; set; }
    }
}
