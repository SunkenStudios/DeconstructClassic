using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.LevelBlock {
    public class ACEEntry {
        public string Name;
        public int ParameterCount;

        public ACEEntry(ByteReader reader) {
            Name = reader.ReadAutoAscii();
            ParameterCount = reader.ReadInt();
        }
    }
}
