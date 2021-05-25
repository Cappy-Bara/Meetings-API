using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingsAPI.Entities;
using MeetingsAPI.Models;

namespace MeetingsAPI.Controllers.Services.Interfaces
{
    public interface IMeetingService
    {
        public Meeting CreateMeetingAndAddToDb(CreateMeetingDto dto);
        public void RemoveMeetingFromDb(int meetingId);
        public List<ReturnMeetingDto> GetAllMeetings();
        public void AddUserToMeetingAndToDb(int meetingId,UserDto dto);

    }
}
