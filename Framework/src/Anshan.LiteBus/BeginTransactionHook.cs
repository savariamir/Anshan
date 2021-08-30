using System;
using System.Threading;
using System.Threading.Tasks;
using Anshan.Core;
using LiteBus.Commands.Abstractions;
using LiteBus.Messaging.Abstractions;

namespace Anshan.LiteBus
{
    public class BeginTransactionHook : ICommandPreHandleHook
    {
        private readonly IUnitOfWork _unitOfWork;

        public BeginTransactionHook(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public Task ExecuteAsync(IMessage message, CancellationToken cancellationToken = new CancellationToken())
        {
            return _unitOfWork.BeginAsync();
        }
    }
}