using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingsAPI.Entities;

namespace MeetingsAPI.Controllers.Services.Interfaces
{
    public interface IMeetingService
    {
        public void CreateMeeting();
        public void RemoveMeeting();
        public List<Meeting> ReturnAllMeetings();
        public void AddUserToMeeting();

    }
}
