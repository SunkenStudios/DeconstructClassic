using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.HlslBlock {
    public class ShaderEntry {
        public string FilePath;

        public int MajorVer;
        public int MinorVer;

        public bool CrossSampling;
        public bool BorderMode;
        public bool FullscreenMode;

        public byte[] Data;

        public ShaderEntry(ByteReader reader) {
            FilePath = reader.ReadAutoAscii();

            MajorVer = reader.ReadInt();
            MinorVer = reader.ReadInt();

            CrossSampling = reader.ReadBool();
            BorderMode = reader.ReadBool();
            FullscreenMode = reader.ReadBool();

            Data = reader.ReadBytes(reader.ReadInt());
        }
    }
}
