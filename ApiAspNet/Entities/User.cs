using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiAspNet.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Title { get; set; }
        [Required, MaxLength(80)]
        public string FirstName { get; set; }
        [Required, MaxLength(80)]
        public string LastName { get; set; }
        [Required, MaxLength(80), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public Role Role { get; set; }

        [JsonIgnore, MaxLength(255)]
        public string PasswordHash { get; set; }
    }
}
