using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gestion_etablissement.Model;
using Microsoft.EntityFrameworkCore;

namespace gestion_etablissement.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Model.Directeur> Directeurs { get; set; }
        public DbSet<Model.Professeur> Professeurs { get; set; }
        public DbSet<Model.Eleve> Eleves { get; set; }
        public DbSet<Model.Cours> Cours { get; set; }

        public DbSet<Model.Classe> Classes { get; set; }
        public DbSet<Model.EmploiDuTemps> EmploiDuTemps { get; set; }

        // Configuration de la connexion à la base de données SQLite.
        protected override void
OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Data/Etablissement.db");
        }
    }
}
