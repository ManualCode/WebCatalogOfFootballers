using System.ComponentModel.DataAnnotations;

namespace WebCatalogOfFootballers.Models
{
    public class Country: IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        public string? CountryName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            var allowedСountries = new string[] { "Россия", "США", "Италия" };

            if (!allowedСountries.Any(c => c == CountryName) | CountryName is null)
                errors.Add(new ValidationResult("Необходимо выбрать страну из предложенных"));

            return errors;
        }
    }
}
