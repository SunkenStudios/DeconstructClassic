using DeconstructClassic.Memory;
using System.Numerics;

namespace DeconstructClassic.ConstructData.ObjectTypes {
    public class CSprite {
        public int Version;

        public Enums.CollisionType CollisionType;
        public bool AutoMirror;
        public bool AutoFlip;
        public int AutoRotations;
        public Enums.AutoRotationType AutoRotationType;
        public bool HideAtStart;
        public int AnimationRoot;
        public Vector2 Skew;

        public bool LockedAnimationAngles;
        public string StartAnim;
        public int StartFrame;

        public CSprite(ByteReader reader) {
            Version = reader.ReadInt();

            CollisionType = (Enums.CollisionType)reader.ReadInt();
            AutoMirror = reader.ReadInt() != 0;
            AutoFlip = reader.ReadInt() != 0;
            AutoRotations = reader.ReadInt();
            AutoRotationType = (Enums.AutoRotationType)reader.ReadInt();
            HideAtStart = reader.ReadBool();
            AnimationRoot = reader.ReadInt();
            Skew = new Vector2(reader.ReadFloat(), reader.ReadFloat());

            LockedAnimationAngles = reader.ReadBool();
            StartAnim = reader.ReadAutoAscii();
            StartFrame = reader.ReadInt();
        }
    }
}
