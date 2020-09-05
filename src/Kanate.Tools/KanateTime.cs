using System;

namespace Kanate.Tools
{
    public static class KanateTime
    {
        public static Func<DateTime> Now = () => DateTime.Now;
        public static Func<DateTime> UtcNow = () => DateTime.UtcNow;
    }
}
