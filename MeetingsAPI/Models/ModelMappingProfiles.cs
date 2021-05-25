using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MeetingsAPI.Entities;

namespace MeetingsAPI.Models
{
    public class ModelMappingProfiles : Profile
    {
        public ModelMappingProfiles()
        {
            CreateMap<UserDto,User>().ReverseMap();
            CreateMap<CreateMeetingDto,Meeting>();
            CreateMap<Meeting, ReturnMeetingDto>();
        }
    }
}
