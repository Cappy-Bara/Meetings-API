using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsAPI.Models
{
    public class ReturnMeetingDto
    {
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public List<UserDto> EnrolledUsers { get; set; }
    }
}
