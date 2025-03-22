using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.HlslBlock {
    public class ShaderTransition {
        public string Name;
        public int ShaderID;
        public ShaderEffectParameter[] Parameters;

        public ShaderTransition(ByteReader reader) {
            Name = reader.ReadAutoAscii();
            ShaderID = reader.ReadInt();
            Name = reader.ReadAutoAscii(); // Weird
            Parameters = new ShaderEffectParameter[reader.ReadInt()];
            for (int i = 0; i < Parameters.Length; i++) {
                Parameters[i] = new ShaderEffectParameter(reader);
            }
        }
    }
}
