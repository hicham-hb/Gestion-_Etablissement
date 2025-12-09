using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using gestion_etablissement.View;

namespace gestion_etablissement.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        // Vue courante (UserControl affiché à droite)
        [ObservableProperty]
        private UserControl currentView;

        // Commandes de navigation
        public ICommand ShowAccueilCommand { get; }
        public ICommand ShowProfesseurCommand { get; }
        public ICommand ShowEleveCommand { get; }
        public ICommand ShowClasseCommand { get; }
        public ICommand ShowCoursCommand { get; }
        public ICommand ShowEmploiDuTempsCommand { get; }

        public ICommand LogoutCommand { get; }

        public HomeViewModel()
        {
            // Vue par défaut : Accueil
            CurrentView = new AccueilView();

            // Navigation entre les différentes vues
            ShowAccueilCommand = new RelayCommand(() => CurrentView = new AccueilView());
            ShowProfesseurCommand = new RelayCommand(() => CurrentView = new ProfesseurView());
            ShowEleveCommand = new RelayCommand(() => CurrentView = new EleveView());
            ShowClasseCommand = new RelayCommand(() => CurrentView = new ClasseView());
           // ShowCoursCommand = new RelayCommand(() => CurrentView = new CoursView());
           // ShowEmploiDuTempsCommand = new RelayCommand(() => CurrentView = new EmploiDuTempsView());

            LogoutCommand = new RelayCommand(Logout);
        }

        private void Logout()
        {
            var result = MessageBox.Show(
                "Voulez-vous vraiment vous déconnecter ?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            var mainWindow = Application.Current.MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Content = new LoginView();
            }
        }
    }
}
