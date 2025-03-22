using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.HlslBlock {
    public class ShaderLayer {
        public int LayoutID;
        public int LayerID;
        public ShaderEffect[] Effects;

        public ShaderLayer(ByteReader reader) {
            LayoutID = reader.ReadInt();
            LayerID = reader.ReadInt();
            Effects = new ShaderEffect[reader.ReadInt()];
            for (int i = 0; i < Effects.Length; i++) {
                Effects[i] = new ShaderEffect(reader);
            }
        }
    }
}
