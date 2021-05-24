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
        public void CreateMeetingAndAddToDb(MeetingDto dto);
        public void RemoveMeetingFromDb(int meetingId);
        public List<Meeting> GetAllMeetings();
        public void AddUserToMeetingAndToDb(AddUserToMeetingDto dto);

    }
}
