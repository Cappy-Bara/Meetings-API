using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MeetingsAPI.Controllers.Services.Interfaces;
using MeetingsAPI.Entities;
using MeetingsAPI.Exceptions;
using MeetingsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingsAPI.Controllers.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly MeetingsDbContext _dbContext;
        private readonly IMapper _mapper;

        public MeetingService(MeetingsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        private void AddUserToDb(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
        private User FindUserInDb(string email)
        {
            return _dbContext.Users.FirstOrDefault(n =>
                n.Email == email
                );
        }
        private Meeting FindMeetingInDb(int meetingId)
        {
            return _dbContext.Meetings
                .Include(n => n.EnrolledUsers)
                .FirstOrDefault(n => n.Id == meetingId);
        }
        private Meeting FindMeetingInDb(CreateMeetingDto dto)
        {
            return _dbContext.Meetings
                .FirstOrDefault(n => n.Name == dto.Name && n.Time == dto.Time);
        }
        private bool MoreThan25PeopleEnrolled(Meeting meeting)
        {
            return meeting.EnrolledUsers.Count() >= 25;
        }
        private bool MeetingExists(CreateMeetingDto dto)
        {
            var meeting = FindMeetingInDb(dto);
            return meeting != null;
        }


        public Meeting CreateMeetingAndAddToDb(CreateMeetingDto dto)
        {
            if(MeetingExists(dto))
                throw new EntityExistsException("This meeting already exists.");
            var meeting = _mapper.Map<Meeting>(dto);
            _dbContext.Add(meeting);
            _dbContext.SaveChanges();
            return meeting;
        }
        public List<ReturnMeetingDto> GetAllMeetings()
        {
            return _mapper.Map<List<ReturnMeetingDto>>
                (_dbContext.Meetings
                .Include(n => n.EnrolledUsers)
                .ToList());
        }
        public void RemoveMeetingFromDb(int meetingId)
        {
            var meeting = FindMeetingInDb(meetingId);
            if (meeting == null)
                throw new NotFoundException("Meeting not found.");
            _dbContext.Meetings.Remove(meeting);
            _dbContext.SaveChanges();
        }
        public void AddUserToMeetingAndToDb(int meetingId, UserDto dto)
        {
            var meeting = FindMeetingInDb(meetingId);
            if (meeting == null)
                throw new NotFoundException("Meeting not found!");
            var user = FindUserInDb(dto.Email);
            if (user == null)
            {
                user = _mapper.Map<User>(dto);
                AddUserToDb(user);
            }
            else
            {
                if(meeting.EnrolledUsers.FirstOrDefault(n => n.Email == user.Email) != null)
                    throw new EntityExistsException("User with this email has already enrolled to the meeting!"); 
            }

            if (MoreThan25PeopleEnrolled(meeting))
                throw new EntityExistsException("Too much users enrolled in the meeting");
            
            meeting.EnrolledUsers.Add(user);
            _dbContext.Meetings.Update(meeting);
            _dbContext.SaveChanges();
        }
    }
}
