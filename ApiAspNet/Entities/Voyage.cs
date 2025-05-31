using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAspNet.Entities
{
    public class Voyage
    {
        [Key]
        public int IdVoyage { get; set; }

        [Display(Name = "Destination"), Required(ErrorMessage = "*"), MaxLength(100)]
        public string Destination { get; set; }

        [Display(Name = "Date depart"), Required(ErrorMessage = "*")]
        public DateTime DateDepart { get; set; }

        [Display(Name = "Date arrivee"), Required(ErrorMessage = "*")]
        public DateTime DateArrivee { get; set; }

        [Display(Name = "Prix"), Required(ErrorMessage = "*")]
        public float Prix { get; set; }
        public int? OffreIdOffre { get; set; }
        [ForeignKey("OffreIdOffre")]
        public virtual Offre Offre { get; set; }
    }
}
