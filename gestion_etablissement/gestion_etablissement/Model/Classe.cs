using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestion_etablissement.Model
{
    public class Classe
    {
        [Key]
        public int Id_classe { get; set; }

        public string Niveau { get; set; }
        public string AnneeScolaire { get; set; }
    }
}
