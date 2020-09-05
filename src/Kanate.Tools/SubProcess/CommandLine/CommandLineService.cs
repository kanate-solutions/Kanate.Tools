using Kanate.Tools.IO;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Kanate.Tools.SubProcess.CommandLine
{
    public sealed class CommandLineService
    {
        private const string EchoEnd = "echo " + End;
        private const string End = "endreadingcommandlineresponse";
        private const string Microsoft = "Microsoft ";
        private const string CommandLinePath = "C:\\Windows\\System32\\cmd.exe";

        private readonly SemaphoreSlim semaphoreSlim;
        private readonly Process cmdProcess;
        private readonly StreamWriter streamWriter;

        private BufferBlock<string> tb;
        private CancellationToken cancellationToken;

        public static CommandLineService CreateForCommandLinePath(string commandLinePath = CommandLinePath)
        {
            if(KanateFile.Exists(commandLinePath))
            {
                return new CommandLineService(commandLinePath);
            }

            throw new ArgumentException("commandLinePath");
        }

        internal CommandLineService(string commandLinePath)
        {
            this.semaphoreSlim = new SemaphoreSlim(1, 1);

            cmdProcess = new Process();

            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = commandLinePath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            cmdProcess.OutputDataReceived += OnOutputDataReceived;
            cmdProcess.ErrorDataReceived += OnOutputDataReceived;

            cmdProcess.StartInfo = processStartInfo;
            cmdProcess.Start();

            streamWriter = cmdProcess.StandardInput;
            cmdProcess.BeginOutputReadLine();
        }

        public async Task<string> ExecuteCommandAsync(string command, CancellationToken cancellationToken)
        {
            string result = string.Empty;

            try
            {
                await semaphoreSlim.WaitAsync(cancellationToken);

                if (cancellationToken.IsCancellationRequested)
                {
                    string temp = result;
                    semaphoreSlim.Release();
                    return temp;
                }

                this.cancellationToken = cancellationToken;

                tb = new BufferBlock<string>();

                streamWriter.WriteLine(command);
                streamWriter.WriteLine(EchoEnd);

                result = await ReadCommandResultsAsync(tb, command, cancellationToken);
            }
            catch(Exception ex)
            {
                result = ex.Message;
            }
            finally
            {
                tb = null;
                semaphoreSlim.Release();
            }

            return result;
        }

        private async Task<string> ReadCommandResultsAsync(BufferBlock<string> bufferBlock, string command, CancellationToken cancellationToken)
        {
            var sb = new StringBuilder(256);

            if (!cancellationToken.IsCancellationRequested)
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                var resultCommand = baseDir[0..^1] + '>' + command;

                while (await bufferBlock.OutputAvailableAsync(cancellationToken))
                {
                    var current = bufferBlock.Receive();

                    if (!current.Contains(Microsoft) && !current.Contains(resultCommand))
                    {
                        sb.Append(current);
                    }

                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }

            return sb.ToString();
        }

        private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data.Contains(End))
                tb?.Complete();
            else
                tb?.Post(e.Data);

            if (this.cancellationToken.IsCancellationRequested)
            {
                tb?.Complete();
            }
        }

        public void Dispose()
        {
            cmdProcess.Close();
            cmdProcess.Dispose();
            streamWriter.Close();
            streamWriter.Dispose();
        }
    }
}
