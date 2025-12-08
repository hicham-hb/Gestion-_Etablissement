using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestion_etablissement.Model
{
    public class Notification
    {
        [Key]
        public int Id_notification { get; set; }

        [Required]
        [MaxLength(200)]
        public string Titre { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Message { get; set; } = string.Empty;

        public DateTime DateCreation { get; set; } = DateTime.Now;

        public bool EstLue { get; set; } = false;

        public TypeNotification Type { get; set; }

        public PrioriteNotification Priorite { get; set; } = PrioriteNotification.Normale;

        // Relations - destinataire
        [ForeignKey(nameof(Utilisateur))]
        public int Id_utilisateur { get; set; }
        public Utilisateur? Utilisateur { get; set; }
    }

    public enum TypeNotification
    {
        Info,
        Rappel,
        Alerte,
        Success,
        Warning,
        Error
    }

    public enum PrioriteNotification
    {
        Basse,
        Normale,
        Haute,
        Urgente
    }
}