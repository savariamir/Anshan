using System;
using System.Threading;
using System.Threading.Tasks;
using Anshan.Core;
using LiteBus.Commands.Abstractions;
using LiteBus.Messaging.Abstractions;

namespace Anshan.LiteBus
{
    public class CommitTransactionHook : ICommandPostHandleHook
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommitTransactionHook(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task ExecuteAsync(IMessage message, CancellationToken cancellationToken = new CancellationToken())
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