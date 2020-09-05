using NUnit.Framework;
using System;

namespace Kanate.Tools.Tests
{
    [TestFixture]
    public class KanateTimeTests
    {
        [Test]
        public void Now_Returns_DateTimeNow_When_Not_S()
        {
            // we have to subtract milliseconds and ignore them during comparison due to different time of invocation
            DateTime dateTimeNow = DateTime.Now;
            var textDate = dateTimeNow
                .AddMilliseconds(-dateTimeNow.Millisecond)
                .ToString("yyyy-MM-ddThh:mm:ss");

            DateTime kanateTimeNow = KanateTime.Now();
            var textKanate = kanateTimeNow
                .AddMilliseconds(-kanateTimeNow.Millisecond)
                .ToString("yyyy-MM-ddThh:mm:ss"); ;


            Assert.AreEqual(textDate, textKanate);
        }

        [Test]
        public void UtcNow_Returns_DateTimeNow_When_Not_S()
        {
            // we have to subtract milliseconds and ignore them during comparison due to different time of invocation
            DateTime dateTimeNow = DateTime.UtcNow;
            var textDate = dateTimeNow
                .AddMilliseconds(-dateTimeNow.Millisecond)
                .ToString("yyyy-MM-ddThh:mm:ss");

            DateTime kanateTimeNow = KanateTime.UtcNow();
            var textKanate = kanateTimeNow
                .AddMilliseconds(-kanateTimeNow.Millisecond)
                .ToString("yyyy-MM-ddThh:mm:ss"); ;


            Assert.AreEqual(textDate, textKanate);
        }

        [Test]
        public void Now_Returns_Stubbed_Date()
        {
            KanateTime.Now = () => new DateTime(2011, 11, 11, 11, 11, 11, 111);

            var subject = KanateTime.Now().ToString("yyyy-MM-ddThh:mm:ss.fff");

            Assert.AreEqual("2011-11-11T11:11:11.111", subject);
        }

        [Test]
        public void UtcNow_Returns_Stubbed_Date()
        {
            KanateTime.UtcNow = () => new DateTime(2011, 11, 11, 11, 11, 11, 111);

            var subject = KanateTime.UtcNow().ToString("yyyy-MM-ddThh:mm:ss.fff");

            Assert.AreEqual("2011-11-11T11:11:11.111", subject);
        }
    }
}
