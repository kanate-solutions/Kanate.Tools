using MediatR;

namespace Kanate.Tools.Messaging
{
    public interface IQueryHandler<TQuery, TReturnType> : IRequestHandler<TQuery, TReturnType>
        where TQuery : Query<TReturnType>
    {
    }
}
