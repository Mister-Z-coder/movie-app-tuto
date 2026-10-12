using System.Globalization;
using System.Text;
namespace MovieApp.Helpers;
public static class SlugExtensions
{
// "Le Fabuleux Destin d'Amélie" -> "le-fabuleux-destin-d-amelie"
public static string ToSlug(this string texte)
{
var decompose = 
texte.ToLowerInvariant().Normalize(NormalizationForm.FormD);
var sb = new StringBuilder();
foreach (var c in decompose)
{
if (CharUnicodeInfo.GetUnicodeCategory(c) == 
UnicodeCategory.NonSpacingMark)
continue; // retire les accents
if (char.IsLetterOrDigit(c))
sb.Append(c);
else if (sb.Length > 0 && sb[^1] != '-')
sb.Append('-');// espaces, apostrophes -> tiret
}
return sb.ToString().Trim('-');
}
}