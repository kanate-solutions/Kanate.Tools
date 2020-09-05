using System.Threading;
using System.Threading.Tasks;

namespace Kanate.Tools.Messaging
{
    public interface IBus
    {
        Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
            where TNotification : ICommand;

        Task<TResponse> Send<TResponse>(Query<TResponse> request, CancellationToken cancellationToken = default);
    }
}
