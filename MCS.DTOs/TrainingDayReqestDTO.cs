using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DTOs
{
    public class TrainingDayReqestDTO
    {
        public int TrainingDayID { get; set; }
        public DateTime TrainingDate { get; set; }
        public string? Note{ get; set; }
        public bool IsClosed { get; set; } //To prevent Edit
        public List<SessionDTO> Sessions { get; set; } = new();


        
    }
}
