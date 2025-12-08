using System;
using System.Linq;
using System.Windows;              // <- pour Application.Current
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using gestion_etablissement.View;

namespace gestion_etablissement.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        // Contexte de la base de données
        private readonly Data.AppDbContext _context;

        // Ces attributs génèrent automatiquement :
        // public string Username { get; set; } avec INotifyPropertyChanged
        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string errorMessage;

        // Commande de connexion
        public ICommand LoginCommand { get; }

        // Constructeur
        public LoginViewModel()
        {
            _context = new Data.AppDbContext();
            LoginCommand = new RelayCommand(Login);
        }

        private void Login()
        {
            // Validation des champs
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Veuillez remplir tous les champs.";
                return;
            }

            try
            {
                // Vérification dans la table Utilisateurs
                var utilisateur = _context.Utilisateurs
                    .FirstOrDefault(u => u.Nom_utilisateur == Username &&
                                         u.Mot_de_passe == Password);

                if (utilisateur == null)
                {
                    ErrorMessage = "Nom d'utilisateur ou mot de passe incorrect.";
                    return;
                }

                // On efface le message d’erreur si la connexion est OK
                ErrorMessage = string.Empty;

                // Si tout est bon → ouvrir la fenêtre principale
                var mainWindow = Application.Current.MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.Content = new HomeView();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Erreur de connexion à la base de données : {ex.Message}";
            }
        }
    }
}
