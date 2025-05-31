﻿using System.ComponentModel.DataAnnotations;

namespace ApiAspNet.Entities
{
    public class Chauffeur
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(80)]
        public string Nom { get; set; }
        [Required, MaxLength(80)]
        public string Prenom { get; set; }
    }
}
