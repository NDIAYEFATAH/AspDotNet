using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Models.offre
{
    public class CreateOffreRequest
    {
        [Required, MaxLength(2000)]
        public string DescriptionOffre { get; set; }

        [Required]
        public float PrixOffre { get; set; }

        [MaxLength(20)]
        public string DisponibiliteOffre { get; set; }

        public int IdAgence { get; set; }
    }
}
