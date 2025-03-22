namespace DeconstructClassic.Memory {
    public class BinaryFile {
        public ByteReader Data;
        public FileType Type;
        public int ID;

        public BinaryFile(ByteReader data, FileType type) {
            Data = data;
            Type = type;
        }

        public byte[] GetData() {
            Data.Seek(0);
            return Data.ReadBytes();
        }

        public enum FileType {
            WAVE
        }
    }
}
