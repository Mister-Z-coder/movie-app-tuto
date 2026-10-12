using System.ComponentModel.DataAnnotations;
namespace MovieApp.Models;
public class Genre
{
public int Id { get; set; }
[Required(ErrorMessage = "Le nom du genre est obligatoire.")]
[StringLength(50, ErrorMessage = "50 caractères maximum.")]
public string Nom { get; set; } = string.Empty;

[StringLength(250)]
public string? Description { get; set; }
// Propriété de navigation : les films de ce genre
public List<Movie> Movies { get; set; } = new();
}