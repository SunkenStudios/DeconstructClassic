using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.AppBlock {
    public class GlobalVariable {
        public string Name = string.Empty;
        public Enums.VariableType Type;
        public object? Value;

        public GlobalVariable(ByteReader reader) {
            Name = reader.ReadAutoAscii();
            Type = (Enums.VariableType)reader.ReadInt();
            Value = reader.ReadAutoAscii();
            if (Type == Enums.VariableType.Number) {
                Value = int.Parse((string)Value);
            }
        }
    }
}
