using Kanate.Tools.Messaging;

namespace Kanate.Tools.SubProcess
{
    public abstract class SubProcessQuery<TContent, TReturnType> : Query<TReturnType>
    {
        public TContent Content { get; }

        public SubProcessQuery(TContent content)
            : base()
        {
            Content = content;
        }
    }
}
