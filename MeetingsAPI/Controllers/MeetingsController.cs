using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingsAPI.Controllers.Services.Interfaces;
using MeetingsAPI.Entities;
using MeetingsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeetingsAPI.Controllers
{
    [Route("meeting")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly IMeetingService _meetingService;

        public MeetingsController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [HttpPost]
        public ActionResult CreateMeeting([FromBody]CreateMeetingDto dto)
        {
            var meeting =_meetingService.CreateMeetingAndAddToDb(dto);
            return Created($"/meeting/{meeting.Id}",meeting);
        }

        [HttpGet]
        public ActionResult<List<Meeting>> GetAllMeetings()
        {
            return Ok(_meetingService.GetAllMeetings());
        }

        [HttpDelete("{meetingId}")]
        public ActionResult RemoveMeeting([FromRoute]int meetingId)
        {
            _meetingService.RemoveMeetingFromDb(meetingId);
            return NoContent();
        }

        [HttpPost("{meetingId}")]
        public ActionResult AddUserToMeeting([FromRoute]int meetingId,[FromBody]UserDto dto)
        {
            _meetingService.AddUserToMeetingAndToDb(meetingId, dto);
            return Ok();
        }

        
    }
}
