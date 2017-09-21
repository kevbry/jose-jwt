using System;
using System.IO;
using System.Text;

namespace UnitTestProject2.util
{
    public static class Helpers
    {
        public static string GetResource(this object scope, string name)
        {
            return ReadResource(scope.GetType(), name);
        }
        public static string ReadResource(Type type, string name)
        {
            using (Stream stream = type.Assembly.GetManifestResourceStream(type, name))
            {
                if (stream == null) throw new FileNotFoundException(name);
                using(StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
