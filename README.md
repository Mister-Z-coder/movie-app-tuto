# 🎬 MovieApp — Tutoriel ASP.NET Core MVC

Application fil rouge du cours **Développement Web : HTML, CSS, JavaScript et ASP.NET Core MVC** (M1 LMD, IFADC).
En trois séances pratiques, les étudiants construisent pas à pas un catalogue de films complet qui mobilise **toutes les notions du polycopié** (chapitres 1 à 5).

> Règle d'or : chaque étape se termine par une application qui compile et tourne, et par un commit Git.

---

## Fonctionnalités

- Catalogue de films en cartes, avec **recherche** (titre, réalisateur), **filtre par genre** et **pagination**
- **Fiche détaillée** à l'URL lisible `/films/1/inception`, avec suggestions du même genre
- **CRUD complet** des films (ajout, modification, suppression) avec formulaires validés côté client et serveur
- **Gestion des genres** (relation un-à-plusieurs Genre → Films) et menu latéral avec compteurs (View Component)
- **API REST JSON** `/api/movies` (GET, POST, PUT, DELETE), testée avec Postman
- **Authentification** avec ASP.NET Core Identity : tout le monde consulte, seuls les utilisateurs connectés écrivent
- Pages d'erreur soignées (404, 500), journalisation structurée, middleware personnalisé

Les données sont stockées dans SQL Server via Entity Framework Core. **Aucune API externe** (pas de TMDB) : 6 genres et 7 films sont insérés par les migrations.

## Technologies

| Outil | Version |
| --- | --- |
| ASP.NET Core MVC | .NET 8 (LTS) |
| Entity Framework Core | 8.0.x (SQL Server) |
| ASP.NET Core Identity | 8.0.x |
| SQL Server | LocalDB ou SQL Server Express |
| Front | Razor, Bootstrap 5, jQuery Validation |
| Tests d'API | Postman |
| IDE | Visual Studio 2022 (charge « Développement web et ASP.NET ») |

## Démarrage rapide

```bash
git clone https://github.com/Mister-Z-coder/movie-app-tuto.git
cd movie-app-tuto/MovieApp

dotnet tool install --global dotnet-ef --version 8.0.10   # une seule fois
sqllocaldb start MSSQLLocalDB                             # Windows avec Visual Studio

dotnet ef database update   # crée la base MovieAppDb avec les données initiales
dotnet run
```

Chaîne de connexion (dans `appsettings.json`) :

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MovieAppDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

Avec SQL Server Express, remplacer `(localdb)\\mssqllocaldb` par `.\\SQLEXPRESS`.

## Le parcours en 3 séances

### Séance 1 — Jeudi : les fondations

| Étape | Durée | Contenu | Polycopié |
| --- | --- | --- | --- |
| 0 | 15 min | Environnement : SDK .NET 8, LocalDB, `dotnet-ef` | 1.1 |
| 1 | 30 min | Création du projet MVC, structure des dossiers, cycle d'une requête | 1.3 à 1.7, 2.1 |
| 2 | 45 min | `Program.cs`, pipeline, middleware `RequestLoggerMiddleware`, court-circuit `/ping` | 2.2, 2.3 |
| 3 | 40 min | `appsettings.json`, `IOptions<MovieAppSettings>`, `ILogger` | 2.5, 2.6 |
| 4 | 45 min | Contrôleurs, routage conventionnel et par attributs, types de résultats | 3.1, 3.2, 3.4, 3.5 |
| 5 | 50 min | `_Layout`, syntaxe Razor, ViewData et ViewBag | 4.1 à 4.3, 4.5, 4.6 |

### Séance 2 — Vendredi : les données

| Étape | Durée | Contenu | Polycopié |
| --- | --- | --- | --- |
| 6 | 45 min | Modèles `Genre` et `Movie`, annotations, validation personnalisée `[DateSortieValide]` | 5.2, 5.3, 5.5 |
| 7 | 60 min | EF Core : `ApplicationDbContext`, Fluent API, données initiales, migrations | 5.7 |
| 8 | 45 min | Couche service `IMovieService`, injection de dépendances, Singleton / Scoped / Transient | 2.4 |
| 9 | 60 min | `MovieIndexViewModel`, vue fortement typée, vue partielle `_MovieCard` | 4.4, 4.6.4, 5.4 |
| 10 | 50 min | Recherche, filtre, pagination, liaison de modèle `[FromQuery]` | 3.6, 4.5, 5.6 |
| 11 | 45 min | Fiche détaillée, `[HttpGet("films/{id:int}/{slug?}")]`, films du même genre | 3.2, 5.4, 5.7 |

### Séance 3 — Samedi : CRUD, API et sécurité

| Étape | Durée | Contenu | Polycopié |
| --- | --- | --- | --- |
| 12 | 90 min | CRUD, `MovieFormViewModel`, Tag Helpers, `[ValidateAntiForgeryToken]`, TempData | 3.3, 4.4, 4.5, 4.6.3, 5.5 |
| 13 | 40 min | Gestion des erreurs : `try/catch`, `UseExceptionHandler`, pages 404 | 3.7, 2.3, 2.6 |
| 14 | 50 min | View Component `GenresMenu`, `GenresController`, `[Bind]` | 4.7, 5.7 |
| 15 | 60 min | API REST `MoviesApiController`, `MovieDto`, tests Postman | 1.4, 3.3, 3.5 |
| 16 | 60 min | ASP.NET Core Identity, `[Authorize]`, `[AllowAnonymous]` | Objectifs du cours |

Chaque étape correspond à un commit `Étape N : …`. Un étudiant en retard peut repartir du commit de l'étape précédente :

```bash
git log --oneline          # repérer le commit de l'étape
git checkout <hash>        # se placer dessus
git switch -c mon-travail  # continuer sur sa propre branche
```

## Architecture

```text
MovieApp/
├── Controllers/
│   ├── HomeController.cs
│   ├── MoviesController.cs
│   ├── GenresController.cs
│   └── Api/MoviesApiController.cs
├── Data/ApplicationDbContext.cs
├── Helpers/SlugExtensions.cs
├── Middlewares/RequestLoggerMiddleware.cs
├── Models/
│   ├── Movie.cs, Genre.cs
│   ├── Settings/MovieAppSettings.cs
│   ├── ViewModels/   (MovieIndex, MovieForm, MovieDetails, GenreMenuItem)
│   ├── Dtos/MovieDto.cs
│   └── Validation/DateSortieValideAttribute.cs
├── Services/IMovieService.cs, MovieService.cs
├── ViewComponents/GenresMenuViewComponent.cs
├── Views/
│   ├── Home/, Movies/, Genres/
│   └── Shared/ (_Layout, _MovieCard, _Alert, _LoginPartial, Components/GenresMenu/Default)
├── wwwroot/
├── appsettings.json
└── Program.cs
```

Trajet d'une requête : **Navigateur / Postman → Pipeline de middlewares → Contrôleur (MVC ou API) → Service → EF Core → SQL Server**.
Le même `MovieService` sert le site MVC (réponses HTML via Razor) et l'API (réponses JSON via des DTO).

## Principales URL

| URL | Description |
| --- | --- |
| `/` | Accueil, 3 derniers films ajoutés |
| `/Movies?recherche=nolan&genreId=1&page=1` | Catalogue filtré |
| `/films/1/inception` | Fiche d'un film |
| `/Movies/Create` | Ajout (connexion requise) |
| `/Genres` | Liste des genres |
| `/a-propos` | Page « À propos » (route par attribut) |
| `/ping` | Health check, court-circuit du pipeline |
| `/Identity/Account/Login` | Connexion |

## API REST

| Méthode | Route | Succès | Erreurs |
| --- | --- | --- | --- |
| GET | `/api/movies?recherche=&genreId=&page=1&taille=10` | 200 + en-tête `X-Total-Count` | — |
| GET | `/api/movies/{id}` | 200 | 404 |
| POST | `/api/movies` | 201 + en-tête `Location` | 400 |
| PUT | `/api/movies/{id}` | 204 | 400, 404 |
| DELETE | `/api/movies/{id}` | 204 | 404 |

Exemple de corps pour `POST /api/movies` :

```json
{
  "titre": "Le Roi Lion",
  "realisateur": "Roger Allers et Rob Minkoff",
  "dateSortie": "1994-06-24",
  "dureeMinutes": 88,
  "note": 9,
  "genreId": 4
}
```

> En TP, l'API n'est pas protégée par authentification. En production, on la sécuriserait par jetons JWT.

## Dépannage

| Symptôme | Correction |
| --- | --- |
| Erreur de connexion à SQL Server | `sqllocaldb start MSSQLLocalDB`, vérifier la chaîne de connexion |
| `dotnet ef` introuvable | `dotnet tool install --global dotnet-ef --version 8.0.10`, rouvrir le terminal |
| « The Genre field is required » | Déclarer la navigation nullable : `public Genre? Genre { get; set; }` |
| `Cannot consume scoped service … from singleton` | Enregistrer le service avec `AddScoped` |
| 400 à l'envoi d'un formulaire | Utiliser `<form asp-action="…" method="post">` (jeton anti-falsification) |
| `[Authorize]` sans effet | `UseAuthentication()` entre `UseRouting()` et `UseAuthorization()` |

## Pour aller plus loin

1. Avis des spectateurs (entité `Avis` liée à `Movie`)
2. Téléversement d'affiche avec `IFormFile`
3. Tri du catalogue par titre, date ou note
4. Rôle `Admin` réservé à la suppression
5. Notation en AJAX avec `fetch` vers l'API
6. API sécurisée par JWT

---

**Enseignant :** Assistant Bakatuasa Bankoledi Jérémie — IFADC, octobre 2026.
Les films fournis sont des exemples pédagogiques (dates indicatives).
