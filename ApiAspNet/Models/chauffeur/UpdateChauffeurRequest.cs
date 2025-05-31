using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Models.chauffeur
{
    public class UpdateChauffeurRequest
    {
        [MaxLength(80)]
        public string Nom { get; set; }

        [MaxLength(80)]
        public string Prenom { get; set; }
        private string replaceEmptyWithNull(string value)
        {
            // replace empty string with null to make field optional 
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }
}
