# Covoiturage

Application de covoiturage en WPF avec une architecture en couches.

## Prérequis
- .NET 10 SDK
- Visual Studio 2026 (ou version compatible)

## Structure du projet
- **Presentation** : interface WPF (vues et navigation)
- **Services** : logique métier (réservations, trajets, utilisateurs, voitures)
- **DataAccess** : accès aux données et modèles (EF Core)

## Fonctionnalités principales
- Authentification/inscription des utilisateurs (passager/conducteur)
- Gestion des trajets et des voitures
- Gestion des réservations (création, mise à jour, suppression)
- Consultation des réservations par trajet ou passager

## Exemples d’utilisation (code)

### Connexion et inscription
```csharp
var auth = new AuthentificationService(dbContext);

// Inscription
var nouvelUtilisateur = new Utilisateur
{
	Nom = "Dupont",
	Prenom = "Sara",
	Email = "sara.dupont@exemple.com",
	MotDePasse = "password",
	Role = "Passager"
};
var ok = auth.Inscription(nouvelUtilisateur);

// Connexion
var utilisateur = auth.Connexion("sara.dupont@exemple.com", "password");
```

### Trajets
```csharp
var trajetService = new TrajetService(dbContext);

// Liste des trajets avec conducteur + voiture
var trajets = trajetService.GetTrajets();

// Ajouter un trajet
trajetService.AjouterTrajet(new Trajet
{
	AdresseDepart = "Tunis",
	AdresseArrivee = "Sousse",
	DateDepart = DateTime.Now.AddDays(1),
	DureeMinutes = 90,
	ConducteurId = 1,
	VoitureId = 2
});
```

### Réservations
```csharp
var reservationService = new ReservationService(dbContext);

// Ajouter une réservation
reservationService.AjouterReservation(new Reservation
{
	PassagerId = 3,
	TrajetId = 5,
	NbPlaces = 2,
	DateReservation = DateTime.Now,
	Statut = "Confirmée",
	Prix = 30m
});

// Réservations d’un passager
var reservationsPassager = reservationService.GetReservationsByPassagerId(3);
```

## Lancer l’application
1. Ouvrir `Covoiturage.slnx` dans Visual Studio.
2. Définir **Presentation** comme projet de démarrage si nécessaire.
3. Exécuter l’application (F5).

## Base de données
Le projet **DataAccess** contient le contexte et les migrations EF Core.
Si une mise à jour est nécessaire, exécuter les migrations depuis la Console du Gestionnaire de packages :

```
Update-Database
```

## Configuration de la base
Le contexte est défini dans `DataAccess/Data/CovoiturageContext.cs` :
```csharp
optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=CovoiturageDB;Trusted_Connection=True;");
```

## Notes
- La navigation démarre sur `MainWindow` qui charge `HomePage`.
- Les services principaux se trouvent dans le dossier **Services**.
