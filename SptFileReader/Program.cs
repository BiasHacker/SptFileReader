using System;
using System.IO;
using System.Text;

namespace SptFileReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new byte[] {
                0x01, 0x00, 0x00, 0x00, 0x06, 0x00, 0x00, 0x00,
                0x83, 0x65, 0x83, 0x58, 0x83, 0x67, 0x0F, 0x00,
                0x00, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06,
                0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E,
                0x0F
            };

            using (var stream = new MemoryStream(data)
                /* File.Open(@"test.spt", FileMode.OpenOrCreate) */)
            {
                var reader = new BinaryReader(stream);
                var sptCount = reader.ReadInt32();

                for (var i = 0; i < sptCount; i++)
                {
                    var length = reader.ReadInt32();
                    var bin = reader.ReadBytes(length);
                    var name = Encoding.Default.GetString(bin);
                    var size = reader.ReadInt32();
                    var packet = reader.ReadBytes(size);
                    Console.WriteLine($"name: {name}, size: {size}");
                    Console.WriteLine(BitConverter.ToString(packet));
                }
            }

            Console.ReadLine();
        }
    }
}
