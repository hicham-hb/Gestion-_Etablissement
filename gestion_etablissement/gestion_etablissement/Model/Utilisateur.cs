using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestion_etablissement.Model
{
    public class Utilisateur
    {
        
        [Key]
        public int Id_utilisateur { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Nom_utilisateur { get; set; }

        public string Mot_de_passe { get; set; }
        public TypeUtilisateur TypeUtilisateur { get; set; }
    }
    public enum TypeUtilisateur
    {
        ELEVE,
        PROFESSEUR,
        DIRECTEUR
    }
}
