using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DTOs
{
    public class PersonDTO
    {
        public int?  PersonID { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Le nom ne peut pas dépasser 50 caractères")]
        public string FirstName { get; set; }= string.Empty;

        [Required(ErrorMessage = "Le prénom est obligatoire")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Le prénom ne peut pas dépasser 50 caractères")]
        public string LastName { get; set; }= string.Empty;

        public string FullName => $"{FirstName} {LastName}";
        [Required(ErrorMessage = "La date de naissance est obligatoire")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [StringLength(20)]
        public string? Phone { get; set; }

        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        [StringLength(100)]
        public string? Email { get; set; }
        [StringLength(250)]
        public string? Address { get; set; }
        [Required]
        [RegularExpression("^[GF]$", ErrorMessage = "Le genre doit être 'G' pour garçon ou 'F' pour Fille")]
        public char? Gender { get; set; } 

    }
}
