using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Entities
{
    public class Offre
    {
        [Key]
        public int IdOffre { get; set; }
        [Display(Name = "Description"), Required(ErrorMessage = "*"), MaxLength(2000)]
        public string DescriptionOffre { get; set; }
        [Display(Name = "Prix"), Required(ErrorMessage = "*")]
        public float PrixOffre { get; set; }

        [Display(Name = "Disponibilte"), MaxLength(20)]
        public string DisponibiliteOffre { get; set; }
        public int IdAgence { get; set; }

        [ForeignKey("IdAgence")]
        public virtual Agence Agence { get; set; }

        public virtual ICollection<Voyage> Voyages { get; set; }
    }
}
