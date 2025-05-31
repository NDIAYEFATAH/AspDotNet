using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Models.Gestionnaire
{
    public class UpdateRequests
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
        public string CNIGestionnaire { get; set; }

        private string replaceEmptyWithNull(string value)
        {
            // replace empty string with null to make field optional 
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }
}
