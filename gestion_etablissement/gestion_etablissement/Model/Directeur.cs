using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestion_etablissement.Model
{
    public class Directeur
    {

        [Key]
        public int Id_directeur { get; set; }

        [ ForeignKey("Utilisateur")]
        public int Id_utilisateur { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Poste { get; set; }
    }
}
