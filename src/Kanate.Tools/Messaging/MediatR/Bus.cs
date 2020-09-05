using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kanate.Tools.Messaging.MediatR
{
    public class Bus : IBus
    {
        private readonly IMediator mediator;

        public Bus(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task Publish<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand
        {
            return mediator.Publish(command, cancellationToken);
        }

        public Task<TResponse> Send<TResponse>(Query<TResponse> query, CancellationToken cancellationToken = default)
        {
            return mediator.Send(query, cancellationToken);
        }
    }
}
