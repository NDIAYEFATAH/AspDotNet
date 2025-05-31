using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Entities
{
    public class Client : User
    {
        [Display(Name = "CNI"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string CniClient { get; set; }
    }
}
