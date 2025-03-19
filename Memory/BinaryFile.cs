namespace DeconstructClassic.Memory {
    public class BinaryFile {
        public ByteReader Data;
        public FileType Type;

        public BinaryFile(ByteReader data, FileType type) {
            Data = data;
            Type = type;
        }

        public enum FileType {
            WAVE
        }
    }
}
