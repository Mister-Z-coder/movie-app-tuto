using System.ComponentModel.DataAnnotations;
namespace MovieApp.Models.Validation;
// Une date de sortie doit être postérieure à la première projection publique
// des frères Lumière (28/12/1895) et pas plus de N ans dans le futur.
public class DateSortieValideAttribute : ValidationAttribute
{
    private static readonly DateTime PremiereProjection = new(1895, 12, 28);
    public int AnneesMaxDansLeFutur { get; set; } = 2;
    protected override ValidationResult? IsValid(object? value,
    ValidationContext context)
    {
        if (value is not DateTime date)
            return ValidationResult.Success; // l'absence de valeur est gérée par [Required]
        var dateMax = DateTime.Today.AddYears(AnneesMaxDansLeFutur);
        if (date < PremiereProjection || date > dateMax)
        {
            return new ValidationResult(ErrorMessage ??
                $"La date doit être comprise entre le {PremiereProjection:dd/MM/yyyy} et le {dateMax:dd/MM/yyyy}.");
        }
        return ValidationResult.Success;
    }
}