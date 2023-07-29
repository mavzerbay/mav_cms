using MAV.Cms.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAV.Cms.Domain.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(MavUser user);
    }
}
