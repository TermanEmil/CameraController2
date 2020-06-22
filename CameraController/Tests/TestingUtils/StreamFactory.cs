using System;
using System.IO;
using System.Text;

namespace TestingUtils
{
    public static class StreamFactory
    {
        public static StreamReader GenerateReader(string value = "")
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            return new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(value)));
        }
    }
}
