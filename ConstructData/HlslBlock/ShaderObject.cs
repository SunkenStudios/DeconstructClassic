using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.HlslBlock {
    public class ShaderObject {
        public int ObjectID;
        public ShaderEffect[] Effects;

        public ShaderObject(ByteReader reader) {
            ObjectID = reader.ReadInt();
            Effects = new ShaderEffect[reader.ReadInt()];
            for (int i = 0; i < Effects.Length; i++) {
                Effects[i] = new ShaderEffect(reader);
            }
        }
    }
}
