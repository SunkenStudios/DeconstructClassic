using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.HlslBlock {
    public class ShaderEffect {
        public int ShaderIndex;
        public string EffectName;
        public Enums.DisableShaderWhen DisableMode;
        public ShaderEffectParameter[] Parameters;

        public ShaderEffect(ByteReader reader) {
            ShaderIndex = reader.ReadInt();
            EffectName = reader.ReadAutoAscii();
            DisableMode = (Enums.DisableShaderWhen)reader.ReadInt();
            Parameters = new ShaderEffectParameter[reader.ReadInt()];
            for (int i = 0; i < Parameters.Length; i++) {
                Parameters[i] = new ShaderEffectParameter(reader);
            }
        }
    }
}
