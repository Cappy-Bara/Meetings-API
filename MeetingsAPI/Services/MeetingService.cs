using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public MeetingService(MeetingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        private Meeting CreateMeetingObject(MeetingDto dto) 
        {
            return new Meeting()
            {
                Name = dto.Name,
                Time = dto.Time
            };
        }
        private User CreateUserObject(string userName, string email) 
        {
            return new User()
            {
                Name = userName,
                Email = email,
            };
        }
        private void AddUserToDb(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
        private User FindUserInDb(string userName, string email)
        {
            return _dbContext.Users.FirstOrDefault(n =>
                n.Name == userName &&
                n.Email == email
                );
        }
        private Meeting FindMeetingInDb(int meetingId)
        {
            return _dbContext.Meetings
                .Include(n => n.EnrolledUsers)
                .FirstOrDefault(n => n.Id == meetingId);
        }
        private bool MoreThan25PeopleEnrolled(Meeting meeting)
        {
            return meeting.EnrolledUsers.Count() >= 25;
        }


        public void CreateMeetingAndAddToDb(MeetingDto dto)
        {
            var meeting = CreateMeetingObject(dto);
            _dbContext.Add(meeting);
            _dbContext.SaveChanges();
        }
        public List<Meeting> GetAllMeetings()
        {
            return _dbContext.Meetings.Include(n => n.EnrolledUsers).ToList();
        }
        public void RemoveMeetingFromDb(int meetingId)
        {
            var meeting = FindMeetingInDb(meetingId);
            if (meeting == null)
                throw new NotFoundException("Meeting not found.");
            _dbContext.Meetings.Remove(meeting);
            _dbContext.SaveChanges();
        }

        //SPRAWDZANIE CZY JEST 25 OSÓB, I CZY UŻYTKOWNIK JEST JUŻ ZAPISANY!
        public void AddUserToMeetingAndToDb(AddUserToMeetingDto dto)
        {
            var meeting = FindMeetingInDb(dto.MeetingId);
            if (meeting == null)
                throw new NotFoundException("Meeting not found!");
            var user = FindUserInDb(dto.UserName,dto.UserEmail);
            if (user == null)
            {
                user = CreateUserObject(dto.UserName, dto.UserEmail);
                AddUserToDb(user);
            }
            else
            {
                if(meeting.EnrolledUsers.FirstOrDefault(n => n.Email == user.Email) != null);                 //nie wiem czy to działa
                    throw new Exception("User with this email already enrolls the course");                  //jak to jest po angielski??
            }

            if (MoreThan25PeopleEnrolled(meeting))
                throw new Exception("Too much users enrolled in the meeting");
            
            meeting.EnrolledUsers.Add(user);
            _dbContext.Meetings.Update(meeting);
            _dbContext.SaveChanges();
        }






    }
}
