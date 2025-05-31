using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Entities
{
    public class Reservation
    {
        [Key]
        public int IdReservation { get; set; }
        [Display(Name = "Date"), Required(ErrorMessage = "*")]
        public DateTime DateReservation { get; set; }
        [Display(Name = "Montant"), Required(ErrorMessage = "*")]
        public float MontantReservation { get; set; }
        [Display(Name = "Statut"), Required(ErrorMessage = "*")]
        public string StatutReservation { get; set; }

        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }

    }
}
