using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace StreamUtils
{
    public static class StreamExtensions
    {
        public static async Task<IEnumerable<byte>> ReadToEndAsync(this Stream stream, CancellationToken ct)
        {
            var buffer = new byte[1024];
            var result = new List<byte>();

            int bytesRead;
            do
            {
                if (ct.IsCancellationRequested)
                    break;

                bytesRead = await stream.ReadAsync(buffer, ct);
                result.AddRange(buffer);
            }
            while (bytesRead == buffer.Length);

            return result;
        }
    }
}
