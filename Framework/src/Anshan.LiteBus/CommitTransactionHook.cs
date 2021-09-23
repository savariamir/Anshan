using System;
using System.Threading;
using System.Threading.Tasks;
using Anshan.Core;
using LiteBus.Commands.Abstractions;
using LiteBus.Messaging.Abstractions;

namespace Anshan.LiteBus
{
    public class CommitTransactionHook : ICommandPostHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommitTransactionHook(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task PostHandleAsync(IHandleContext<ICommandBase> context)
        {
            try
            {
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}