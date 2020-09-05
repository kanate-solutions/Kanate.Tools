using System;
using System.IO;

namespace Kanate.Tools.IO
{
    public static class KanateFile
    {
        public static Func<string, bool> DefaultExistsFunc = File.Exists;
        public static bool Exists(string filePath) => DefaultExistsFunc(filePath);
    }
}
