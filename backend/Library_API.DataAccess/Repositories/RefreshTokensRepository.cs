using Library_API.Core.Abstractions;
using Library_API.Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.DataAccess.Repositories
{
    public class RefreshTokensRepository: Repository<RefreshTokenModel>, IRefreshTokensRepository
    {
        public RefreshTokensRepository(LibraryDbContext context)
            : base(context)
        {
        }

        public async Task<RefreshTokenModel> GetNotExpiredToken(Guid userId)
        {
            var refreshToken = await _context.RefreshTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(rt => rt.UserId == userId && rt.Expiration > DateTime.UtcNow);
            return refreshToken;
        }

        public async Task<bool> IsExistByUserId(Guid id)
        {
            return await _context.RefreshTokens.AnyAsync(rt => rt.UserId == id);
        }
    }
}
