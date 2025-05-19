using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Models.Flottes
{
    public class UpdateRequest
    {
        [Required(ErrorMessage = "*"), MaxLength(80)]
        public string TypeFlotte { get; set; }
        [Required(ErrorMessage = "*"), MaxLength(80)]
        public string MatriculeFlotte { get; set; }

        private string replaceEmptyWithNull(string value)
        {
            // replace empty string with null to make field optional 
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }
}
