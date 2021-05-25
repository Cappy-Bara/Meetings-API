using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MeetingsAPI.Entities;

namespace MeetingsAPI.Models.Validators
{
    public class CreateMeetingDtoValidator : AbstractValidator<CreateMeetingDto>
    {
        public CreateMeetingDtoValidator(MeetingsDbContext dbContext)
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(r => r.Time)
                .NotEmpty();
            RuleFor(r => r.Description)
                .MaximumLength(500);
            RuleFor(r => r.Place)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
