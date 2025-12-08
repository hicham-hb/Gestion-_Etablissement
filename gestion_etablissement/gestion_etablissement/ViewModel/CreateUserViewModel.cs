        using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using gestion_etablissement.Data;
using gestion_etablissement.Model;
using gestion_etablissement.Services;

namespace gestion_etablissement.ViewModel
{
    public partial class CreateUserViewModel : ObservableObject
    {
        private readonly AppDbContext _context;

        [ObservableProperty]
        private string nom = string.Empty;

        [ObservableProperty]
        private string prenom = string.Empty;

        [ObservableProperty]
        private string nomUtilisateur = string.Empty;

        [ObservableProperty]
        private string motDePasse = string.Empty;

        [ObservableProperty]
        private TypeUtilisateur typeUtilisateur;

        [ObservableProperty]
        private string message = string.Empty;

        public CreateUserViewModel()
        {
            _context = new AppDbContext();
        }

        [RelayCommand]
        private void CreateUser()
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(NomUtilisateur) || string.IsNullOrWhiteSpace(MotDePasse))
                {
                    Message = "? Tous les champs sont obligatoires.";
                    return;
                }

                // Vérifier si le nom d'utilisateur existe déjà
                if (_context.Utilisateurs.Any(u => u.Nom_utilisateur == NomUtilisateur))
                {
                    Message = "? Ce nom d'utilisateur existe déjà.";
                    return;
                }

                // Créer l'utilisateur avec mot de passe hashé
                var utilisateur = new Utilisateur
                {
                    Nom = Nom,
                    Prenom = Prenom,
                    Nom_utilisateur = NomUtilisateur,
                    Mot_de_passe = PasswordHasher.HashPassword(MotDePasse), // ? Hash le mot de passe
                    TypeUtilisateur = TypeUtilisateur
                };

                _context.Utilisateurs.Add(utilisateur);
                _context.SaveChanges();

                Message = "? Utilisateur créé avec succès !";

                // Réinitialiser les champs
                Nom = string.Empty;
                Prenom = string.Empty;
                NomUtilisateur = string.Empty;
                MotDePasse = string.Empty;
            }
            catch (Exception ex)
            {
                Message = $"? Erreur : {ex.Message}";
            }
        }
    }
}