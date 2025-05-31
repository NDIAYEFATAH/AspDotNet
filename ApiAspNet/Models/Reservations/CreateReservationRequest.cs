using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Models.Reservations
{
    public class CreateReservationRequest
    {
        [Required]
        public DateTime DateReservation { get; set; }

        [Required]
        public float MontantReservation { get; set; }

        [Required]
        public string StatutReservation { get; set; }

        [Required]
        public int ClientId { get; set; }
    }
}
