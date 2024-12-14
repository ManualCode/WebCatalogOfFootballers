using System.ComponentModel.DataAnnotations;

namespace WebCatalogOfFootballers.Models
{
    public class TeamName : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        //[Required(ErrorMessage = "Необходимо указать команду")]
        public string? NameTeam { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (NameTeam is null)
                errors.Add(new ValidationResult("Необходимо указать команду"));
            return errors;
        }
    }
}
