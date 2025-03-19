using DeconstructClassic.Memory;
using System.Numerics;

namespace DeconstructClassic.ConstructData.ImageBlock {
    public class ImageEntry {
        public int ID;
        public Vector2 Hotspot;
        public ActionPoint[] ActionPoints;
        public byte[] Data;

        public int CollisionMaskWidth;
        public int CollisionMaskHeight;
        public int CollisionMaskPitch;
        public byte[] CollisionMaskData;

        public ImageEntry(ByteReader reader) {
            ID = reader.ReadInt();
            Hotspot = new Vector2(reader.ReadInt(), reader.ReadInt());
            ActionPoints = new ActionPoint[reader.ReadInt()];
            for (int i = 0; i < ActionPoints.Length; i++) {
                ActionPoints[i] = new ActionPoint(reader);
            }
            Data = reader.ReadBytes(reader.ReadInt());

            CollisionMaskWidth = reader.ReadInt();
            CollisionMaskHeight = reader.ReadInt();
            CollisionMaskPitch = reader.ReadInt();
            CollisionMaskData = reader.ReadBytes(CollisionMaskPitch * CollisionMaskHeight);
        }
    }
}
