using Kanate.Tools.Messaging;
using Kanate.Tools.Messaging.MediatR;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace Kanate.Tools.Tests.Messaging.MediatR
{
    [TestFixture]
    public class BusTests
    {
        private IMediator mediator;
        private ICommand command;
        private Query<string> query;

        private Bus target;

        [SetUp]
        public void Before_Each_Test()
        {
            this.query = Substitute.For<Query<string>>();
            this.command = Substitute.For<ICommand>();

            this.mediator = Substitute.For<IMediator>();
            this.mediator.Send(this.query, Arg.Any<CancellationToken>()).Returns("queryResponse");

            this.target = new Bus(this.mediator);
        }

        [Test]
        public async Task Send_Returns_MediatR_Response()
        {
            var cancellationToken = CancellationToken.None;

            string response = await this.target.Send(this.query, cancellationToken);

            await this.mediator.Received().Send(this.query, Arg.Is(cancellationToken));

            Assert.AreEqual(response, "queryResponse");
        }

        [Test]
        public async Task Send_Returns_MediatR_Response_With_Default_Cancellation_Token()
        {
            string response = await this.target.Send(this.query);

            await this.mediator.Received().Send(this.query, Arg.Is(default(CancellationToken)));

            Assert.AreEqual(response, "queryResponse");
        }

        [Test]
        public async Task Publish_Invokes_MediatR_Publish()
        {
            var cancellationToken = CancellationToken.None;

            await this.target.Publish(this.command, cancellationToken);

            await this.mediator.Received().Publish(this.command, Arg.Is(cancellationToken));
        }

        [Test]
        public async Task Publish_Invokes_MediatR_Publish_With_Default_Cancellation_Token()
        {
            await this.target.Publish(this.command);

            await this.mediator.Received().Publish(this.command, Arg.Is(default(CancellationToken)));
        }
    }
}
