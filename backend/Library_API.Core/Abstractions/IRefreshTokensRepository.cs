using Library_API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.Core.Abstractions
{
    public interface IRefreshTokensRepository : IRepository<RefreshTokenModel>
    {
        Task<RefreshTokenModel> GetNotExpiredToken(Guid userId);
        Task<bool> IsExistByUserId(Guid id);
    }
}
