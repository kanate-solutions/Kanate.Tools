using Kanate.Tools.Messaging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kanate.Tools.SubProcess.CommandLine
{
    public class CommandLineSubProcessQueryHandler : IQueryHandler<CommandLineSubProcessQuery, string>, IDisposable
    {
        private readonly CommandLineService commandLineService;

        public CommandLineSubProcessQueryHandler(CommandLineService commandLineService)
        {
            this.commandLineService = commandLineService;
        }

        public Task<string> Handle(CommandLineSubProcessQuery query, CancellationToken cancellationToken)
        {
            return this.commandLineService.ExecuteCommandAsync(query.Content, cancellationToken);
        }

        #region IDisposable

        public void Dispose()
        {
            this.commandLineService.Dispose();
        }

        #endregion
    }
}
