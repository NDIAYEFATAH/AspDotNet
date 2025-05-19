using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Models.Voyages
{
    public class CreateRequestV
    {

        [Display(Name = "Destination"), Required(ErrorMessage = "*"), MaxLength(100)]
        public string Destination { get; set; }

        [Display(Name = "Date depart"), Required(ErrorMessage = "*")]
        public DateTime DateDepart { get; set; }

        [Display(Name = "Date arrivee"), Required(ErrorMessage = "*")]
        public DateTime DateArrivee { get; set; }

        [Display(Name = "Prix"), Required(ErrorMessage = "*")]
        public float Prix { get; set; }
    }
}
