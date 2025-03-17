﻿using AuthenticationService.Domain.Entities;
using AuthenticationService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.ServiceInterfaces
{
    public interface IJwtInstrService
    {
        string GenerateJwtToken(Instructor instructor);
    }
}
