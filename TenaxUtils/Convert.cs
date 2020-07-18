using System.Drawing;
using System.IO;

namespace TenaxUtils
{
    public static class TenaxConvert
    {
        public static Icon BytesToIcon(byte[] bytes)
        {
            using (var memoryStram = new MemoryStream(bytes))
            {
                return new Icon(memoryStram);
            }
        }

        public static byte[] IconToBytes(Icon icon)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                icon.Save(ms);
                return ms.ToArray();
            }
        }
    }
}
