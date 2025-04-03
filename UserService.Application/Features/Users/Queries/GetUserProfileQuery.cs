﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTO.UserViewDto;

namespace UserService.Application.Features.Users.Queries
{
    public class GetUserProfileQuery:IRequest<UserProfileDto>
    {
        public Guid UserId { get; set; }
    }
}
