using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.LevelBlock {
    public class AnimationImage {
        public float FrameTime;
        public int Image;

        public AnimationImage(ByteReader reader) {
            FrameTime = reader.ReadFloat();
            Image = reader.ReadInt();
        }
    }
}
