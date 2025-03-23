using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.ObjectTypes {
    public class CTiledBackground {
        public int Version;

        public int ImageID;

        public CTiledBackground(ByteReader reader) {
            Version = reader.ReadInt();

            ImageID = reader.ReadInt();
        }
    }
}
