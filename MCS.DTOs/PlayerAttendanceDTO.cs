using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DTOs
{
    public class PlayerAttendanceDTO
    {
        public int PlayerID { get; set; } 
        public string PlayerName { get; set; }
        public bool isPresent { get; set; }
        public string Note { get; set; }
    }
}
