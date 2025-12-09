using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestion_etablissement_1.Model
{
    public class Cours
    {
        [Key]
        public int Id_cours { get; set; }

        public string NomMatiere { get; set; }
        public string Salle { get; set; }
        

        public DateTime Date_Debut { get; set; }
        public DateTime Date_Fin { get; set; }

        public int Duree { get; set; }
    }
}
