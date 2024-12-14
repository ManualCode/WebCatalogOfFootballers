using System.ComponentModel.DataAnnotations;

namespace WebCatalogOfFootballers.Models
{
    public class Footballer
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Необходимо указать имя")]
        public string? FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Необходимо указать фамилию")]
        public string? LastName { get; set; }

        [Display(Name = "Пол")]
        public Gender? Gender { get; set; }

        [Display(Name = "Дата рождения")]
        [Required(ErrorMessage = "Необходимо указать дату рождения")]
        public DateOnly? Birthday { get; set; }

        [Display(Name = "Команда")]
        public TeamName? TeamName { get; set; }

        [Display(Name = "Страна")]
        public Country? Country { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
