using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using gestion_etablissement_1.Data;
using gestion_etablissement_1.Model;

namespace gestion_etablissement_1.ViewModel
{
    public partial class EleveViewModel : ObservableObject
    {
        private readonly AppDbContext _context;

        [ObservableProperty]
        private ObservableCollection<Eleve> eleves;

        [ObservableProperty]
        private Eleve selectedEleve;

        // Champs du formulaire (backing fields en camelCase)
        [ObservableProperty]
        private string eleveNom;

        [ObservableProperty]
        private string elevePrenom;

        [ObservableProperty]
        private string eleveAdresse;

        [ObservableProperty]
        private DateTime eleveDate_naissance = DateTime.Today;

        [ObservableProperty]
        private string errorMessage;

        [ObservableProperty]
        private string successMessage;

        private bool _confirmationSuppression = false;

        public IRelayCommand AddCommand { get; }
        public IRelayCommand EditCommand { get; }
        public IRelayCommand DeleteCommand { get; }

        public EleveViewModel()
        {
            _context = new AppDbContext();

            LoadEleve();

            AddCommand = new RelayCommand(AddEleve);
            EditCommand   = new RelayCommand(EditEleve);
            DeleteCommand = new RelayCommand(DeleteEleve);
        }

        private void LoadEleve()
        {
            var list = _context.Eleves.ToList();
            Eleves = new ObservableCollection<Eleve>(list);
        }

        // Appelé automatiquement quand SelectedEleve change
        partial void OnSelectedEleveChanged(Eleve value)
        {
            if (value != null)
            {
                EleveNom = value.Nom;
                ElevePrenom = value.Prenom;
                EleveAdresse = value.adresse;
                EleveDate_naissance = value.Date_naissance;

                ErrorMessage = string.Empty;
                SuccessMessage = string.Empty;
                _confirmationSuppression = false;
            }
            else
            {
                ResetForm();
            }
        }

        private void AddEleve()
        {
            // Validation des champs
            if (string.IsNullOrWhiteSpace(EleveNom) ||
                string.IsNullOrWhiteSpace(ElevePrenom) ||
                string.IsNullOrWhiteSpace(EleveAdresse))
            {
                ErrorMessage = "Veuillez remplir tous les champs.";
                SuccessMessage = string.Empty;
                return;
            }

            try
            {
                var e = new Eleve
                {
                    Nom = EleveNom,
                    Prenom = ElevePrenom,
                    adresse = EleveAdresse,
                    Date_naissance = EleveDate_naissance
                };

                _context.Eleves.Add(e);
                _context.SaveChanges();

                Eleves.Add(e);
                ResetForm();

                SuccessMessage = "Élève ajouté avec succès.";
                ErrorMessage = string.Empty;
                _confirmationSuppression = false;
            }
            catch (Exception ex)
            {
                ErrorMessage = "Échec de l'ajout : " + ex.Message;
                SuccessMessage = string.Empty;
            }
        }
        private void EditEleve()
        {
            if (SelectedEleve == null)
            {
                ErrorMessage = "Sélectionnez un élève à modifier.";
                SuccessMessage = string.Empty;
                return;
            }

            if (string.IsNullOrWhiteSpace(EleveNom)
                || string.IsNullOrWhiteSpace(ElevePrenom)
                || string.IsNullOrWhiteSpace(EleveAdresse))
            {
                ErrorMessage = "Veuillez remplir tous les champs.";
                SuccessMessage = string.Empty;
                return;
            }

            try
            {
                // mettre à jour l'élève sélectionné avec les valeurs du formulaire
                SelectedEleve.Nom = EleveNom;
                SelectedEleve.Prenom = ElevePrenom;
                SelectedEleve.adresse = EleveAdresse;
                SelectedEleve.Date_naissance = EleveDate_naissance;

                _context.SaveChanges();
                LoadEleve();   // rafraîchir la liste à partir de la BD

                ResetForm();

                SuccessMessage = "Élève modifié avec succès.";
                ErrorMessage = string.Empty;
                _confirmationSuppression = false;
            }
            catch (Exception ex)
            {
                ErrorMessage = "Échec de la modification : " + ex.Message;
                SuccessMessage = string.Empty;
            }
        }
        private void DeleteEleve()
        {
            if (SelectedEleve == null)
            {
                ErrorMessage = "Sélectionnez un élève à supprimer.";
                SuccessMessage = string.Empty;
                _confirmationSuppression = false;
                return;
            }

            if (!_confirmationSuppression)
            {
                ErrorMessage =
                    $"Souhaitez-vous vraiment supprimer l'élève « {SelectedEleve.Nom} {SelectedEleve.Prenom} » ? Recliquez sur Supprimer pour confirmer.";
                SuccessMessage = string.Empty;
                _confirmationSuppression = true;
                return;
            }

            try
            {
                _context.Eleves.Remove(SelectedEleve);
                _context.SaveChanges();

                Eleves.Remove(SelectedEleve);
                SelectedEleve = null;
                ResetForm();

                SuccessMessage = "Élève supprimé avec succès.";
                ErrorMessage = string.Empty;
                _confirmationSuppression = false;
            }
            catch (Exception ex)
            {
                ErrorMessage = "Échec de la suppression : " + ex.Message;
                SuccessMessage = string.Empty;
                _confirmationSuppression = false;
            }
        }


        private void ResetForm()
        {
            EleveNom = string.Empty;
            ElevePrenom = string.Empty;
            EleveAdresse = string.Empty;
            EleveDate_naissance = DateTime.Today;
            ErrorMessage = string.Empty;
            SuccessMessage = string.Empty;
            SelectedEleve = null;
        }
    }
}
