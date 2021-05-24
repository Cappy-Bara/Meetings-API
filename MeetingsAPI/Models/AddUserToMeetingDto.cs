using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsAPI.Models
{
    public class AddUserToMeetingDto
    {
        public int MeetingId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
