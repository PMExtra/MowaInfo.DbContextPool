using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace MowaInfo.DbContextPool
{
    public class DbContextPool<TContext> : Microsoft.EntityFrameworkCore.Internal.DbContextPool<TContext> where TContext : DbContext
    {
        [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
        public DbContextPool(DbContextOptions<TContext> options) : base(options)
        {
        }
    }
}
