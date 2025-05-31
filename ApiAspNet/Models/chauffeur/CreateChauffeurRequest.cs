using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Models.chauffeur
{
    public class CreateChauffeurRequest
    {
        [Required, MaxLength(80)]
        public string Nom { get; set; }

        [Required, MaxLength(80)]
        public string Prenom { get; set; }

    }
}
