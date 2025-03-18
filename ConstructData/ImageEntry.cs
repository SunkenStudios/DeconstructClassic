using DeconstructClassic.Memory;
using System.Numerics;

namespace DeconstructClassic.ConstructData
{
    public class ImageEntry
    {
        public int ID;
        public Vector2 Hotspot;
        public Vector2[] ActionPoints;
        public byte[] Data;

        public int CollisionMaskWidth;
        public int CollisionMaskHeight;
        public int CollisionMaskPitch;
        public byte[] CollisionMaskData;

        public ImageEntry(ByteReader reader)
        {
            ID = reader.ReadInt();
            Hotspot = new Vector2(reader.ReadInt(), reader.ReadInt());
            ActionPoints = new Vector2[reader.ReadInt()];
            for (int i = 0; i < ActionPoints.Length; i++)
                ActionPoints[i] = new Vector2(reader.ReadInt(), reader.ReadInt());
            Data = reader.ReadBytes(reader.ReadInt());

            CollisionMaskWidth = reader.ReadInt();
            CollisionMaskHeight = reader.ReadInt();
            CollisionMaskPitch = reader.ReadInt();
            CollisionMaskData = reader.ReadBytes(CollisionMaskPitch * CollisionMaskHeight);
        }
    }
}
