using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestion_etablissement.Model
{
   public class Eleve
    {
        [Key]
        public int Id_eleve { get; set; }

        [ForeignKey("Utilisateur")]
        public int Id_utilisateur { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string adresse { get; set; }
        public DateTime Date_naissance { get; set; }
    }
}
