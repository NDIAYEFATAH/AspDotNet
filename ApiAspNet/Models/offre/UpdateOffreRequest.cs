using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Models.offre
{
    public class UpdateOffreRequest
    {
        [MaxLength(2000)]
        public string DescriptionOffre { get; set; }

        public float? PrixOffre { get; set; }

        [MaxLength(20)]
        public string DisponibiliteOffre { get; set; }

        public int? IdAgence { get; set; }
        private string replaceEmptyWithNull(string value)
        {
            // replace empty string with null to make field optional 
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }
}
