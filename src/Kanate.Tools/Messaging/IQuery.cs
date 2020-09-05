using MediatR;
using System;

namespace Kanate.Tools.Messaging
{
    public abstract class Query<TReturnType> : IRequest<TReturnType>
    {
        public DateTime CreatedAt { get; }

        protected Query()
        {
            CreatedAt = KanateTime.UtcNow();
        }
    }
}
