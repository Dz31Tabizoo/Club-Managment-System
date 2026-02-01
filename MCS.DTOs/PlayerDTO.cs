using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DTOs
{
    public class PlayerDTO : PersonDTO
    {
        [Required(ErrorMessage = "La catégorie est obligatoire")]
        public int CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public  bool isActive { get; set; }
    }
}
