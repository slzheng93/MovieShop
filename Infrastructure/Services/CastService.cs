﻿using ApplicationCore.Contracts.Servicces;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CastService : ICastService
    {
        Task<CastModel> ICastService.GetCastDetails(int id)
        {
            throw new NotImplementedException();
        }
    }

}
