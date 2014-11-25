using System;
using System.IO.Abstractions;

namespace TestableXmlSerialization
{
    static internal class FileBaseExtensions
    {
        public static void SafeAction(this FileBase fileBase, string file, Action<string> action)
        {
            if (fileBase == null) throw new ArgumentNullException("fileBase");
            if (string.IsNullOrEmpty(file)) throw new ArgumentNullException("file");
            if (action == null) throw new ArgumentNullException("action");

            var tmp = String.Concat(file, ".tmp");
            action(tmp);
            if (!fileBase.Exists(tmp)) throw new InvalidOperationException("action must create a file");

            //fileBase.Replace(tmp, file, null); write a TestHelper for Replace and make a pull request first
            fileBase.Delete(file);
            fileBase.Move(tmp, file);
        }
    }
}