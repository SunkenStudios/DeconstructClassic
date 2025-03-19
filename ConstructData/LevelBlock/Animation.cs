using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.LevelBlock {
    public class Animation {
        public int ID;
        public string Name;
        public int Tag;

        public float Speed;
        public bool HasAngle;
        public float Angle;
        public int RepeatCount;
        public int RepeatTo;
        public bool PingPong;

        public AnimationImage[] AnimationImages;
        public Animation[] SubAnimations;

        public Animation(ByteReader reader) {
            ID = reader.ReadInt();
            Name = reader.ReadAutoAscii();
            Tag = reader.ReadInt();

            Speed = reader.ReadFloat();
            HasAngle = reader.ReadBool();
            Angle = reader.ReadFloat();
            RepeatCount = reader.ReadInt();
            RepeatTo = reader.ReadInt();
            PingPong = reader.ReadInt() != 0;

            AnimationImages = new AnimationImage[reader.ReadInt()];
            for (int i = 0; i < AnimationImages.Length; i++) {
                AnimationImages[i] = new AnimationImage(reader);
            }

            SubAnimations = new Animation[reader.ReadInt()];
            for (int i = 0; i < SubAnimations.Length; i++) {
                SubAnimations[i] = new Animation(reader);
            }
        }
    }
}
