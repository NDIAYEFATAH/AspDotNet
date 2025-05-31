using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Entities
{
    public class Gestionnaire : User
    {
        [Display(Name = "CNI"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string CNIGestionnaire { get; set; }

        // Relation avec Agence (si elle existe)
        public virtual ICollection<Agence> Agences { get; set; }
    }
}
