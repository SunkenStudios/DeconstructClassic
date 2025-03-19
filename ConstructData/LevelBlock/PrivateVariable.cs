using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.LevelBlock {
    public class PrivateVariable {
        public string Name = string.Empty;
        public Enums.VariableType Type;

        public PrivateVariable(ByteReader reader) {
            Name = reader.ReadAutoAscii();
            Type = (Enums.VariableType)reader.ReadInt();
        }
    }
}
