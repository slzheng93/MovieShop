using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Servicces
{
    public interface ICastService
    {
        Task<CastDeatilModel> GetCastDetails(int id);
    }
}
