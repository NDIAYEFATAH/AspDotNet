using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Models.client
{
    public class UpdateClientRequest
    {
        [MaxLength(20)]
        public string Title { get; set; }

        [MaxLength(80)]
        public string FirstName { get; set; }

        [MaxLength(80)]
        public string LastName { get; set; }

        [MaxLength(80), EmailAddress]
        public string Email { get; set; }

        [MaxLength(255)]
        public string Password { get; set; }

        [MaxLength(20)]
        public string CniClient { get; set; }
        private string replaceEmptyWithNull(string value)
        {
            // replace empty string with null to make field optional 
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }
}
