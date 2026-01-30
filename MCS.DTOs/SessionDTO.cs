using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DTOs
{
    public class SessionDTO
    {
        public int SessionID { get; set; }
        public int CategoryID { get; set; }
        public string GategoryName { get; set; }
        public int CoachID { get; set; }
        public string CoachName { get; set; }
        public List<PlayerAttendanceDTO> AttendanceList { get; set; } = new();
    }
}
