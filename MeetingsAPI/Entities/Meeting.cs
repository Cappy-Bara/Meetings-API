using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsAPI.Entities
{
    public class Meeting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<User> EnrolledUsers { get; set; }
    }
}
