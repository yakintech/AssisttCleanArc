using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assistt.Domain.Models;


namespace Assistt.Infrastructure.Repositories
{
    public interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
    {
    }
}
