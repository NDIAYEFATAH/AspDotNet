using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Models.Flottes
{
    public class CreateRequest
    {
        [Required(ErrorMessage = "*"), MaxLength(80)]
        public string TypeFlotte { get; set; }
        [ Required(ErrorMessage = "*"), MaxLength(80)]
        public string MatriculeFlotte { get; set; }
    }
}
