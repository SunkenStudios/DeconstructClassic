using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.HlslBlock {
    public class ShaderEffectParameter {
        public Enums.EffectParameterType Type;
        public string Name;
        public string VariableName;
        public string Value;

        public ShaderEffectParameter(ByteReader reader) {
            Type = (Enums.EffectParameterType)reader.ReadInt();
            Name = reader.ReadAutoAscii();
            VariableName = reader.ReadAutoAscii();
            Value = reader.ReadAutoAscii();
        }
    }
}
