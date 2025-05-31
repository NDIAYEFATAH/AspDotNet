using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Models.client
{
    public class CreateClientRequest
    {
        [Required, MaxLength(20)]
        public string Title { get; set; }

        [Required, MaxLength(80)]
        public string FirstName { get; set; }

        [Required, MaxLength(80)]
        public string LastName { get; set; }

        [Required, MaxLength(80), EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(255)]
        public string Password { get; set; }

        [Required, MaxLength(20)]
        public string CniClient { get; set; }
    }
}
