using Ionic.Zlib;
using System.Linq;

namespace Helpers
{
    public static class ErpCompress
    {
        public static byte[] Compress(byte[] data)
        {
            var output = ZlibStream.CompressBuffer(data);
            return output.ToArray();
        }
        public static byte[] DeCompress (byte[] data)
        {
            var output = ZlibStream.UncompressBuffer(data);
            return output.ToArray();
        }
    }
}
