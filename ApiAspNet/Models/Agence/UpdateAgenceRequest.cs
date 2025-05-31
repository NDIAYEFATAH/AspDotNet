using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Models.Agence
{
    public class UpdateAgenceRequest
    {
        [MaxLength(150)]
        public string AdresseAgence { get; set; }

        public float? Longitude { get; set; }

        public float? Latitude { get; set; }

        [MaxLength(20)]
        public string NineaGestionnaire { get; set; }

        [MaxLength(20)]
        public string RccmGestionnaire { get; set; }

        public int? IdGestionnaire { get; set; }
    }
}
