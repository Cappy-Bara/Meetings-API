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
        public ActionResult CreateMeeting([FromBody]MeetingDto dto)
        {
            _meetingService.CreateMeetingAndAddToDb(dto);
            return Ok();        //Zmienić na created
        }

        [HttpGet]
        public ActionResult<List<Meeting>> GetAllMeetings()
        {
            return Ok(_meetingService.GetAllMeetings());
        }

        [HttpDelete]
        public ActionResult RemoveMeeting([FromBody]int meetingId)
        {
            _meetingService.RemoveMeetingFromDb(meetingId);
            return NoContent();
        }

        [HttpPost("user")]
        public ActionResult AddUserToMeeting([FromBody]AddUserToMeetingDto dto)
        {
            _meetingService.AddUserToMeetingAndToDb(dto);
            return Ok();
        }

        
    }
}
