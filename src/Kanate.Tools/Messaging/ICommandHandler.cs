using MediatR;

namespace Kanate.Tools.Messaging
{
    public interface ICommandHandler<T> : INotificationHandler<T>
        where T : ICommand
    {
    }
}
