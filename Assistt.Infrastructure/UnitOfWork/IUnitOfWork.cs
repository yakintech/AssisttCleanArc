using Assistt.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistt.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IUserRepository Users { get; }

        IRefreshTokenRepository RefreshTokens { get; }
        void Commit();
    }
}
