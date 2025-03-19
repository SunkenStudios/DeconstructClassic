using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeconstructClassic.Memory
{
    public class BinaryFile
    {
        public ByteReader Data;
        public FileType Type;

        public BinaryFile(ByteReader data, FileType type)
        {
            Data = data;
            Type = type;
        }

        public enum FileType
        {
            WAVE
        }
    }
}
