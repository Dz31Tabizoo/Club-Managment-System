using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DTOs
{
    public class PlayerDTO : PersonDTO
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public  bool isActive { get; set; }
    }
}
