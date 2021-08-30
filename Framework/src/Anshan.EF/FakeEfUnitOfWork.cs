using System.Threading.Tasks;
using Anshan.Core;
using Microsoft.EntityFrameworkCore;

namespace Anshan.EF
{
    public class FakeEfUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private readonly TContext _context;

        public FakeEfUnitOfWork(TContext context)
        {
            _context = context;
        }

        public Task BeginAsync()
        {
            return _context.Database.BeginTransactionAsync();
        }

        public Task CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task RollbackAsync()
        {
            return _context.Database.CurrentTransaction.RollbackAsync();
        }
    }
}