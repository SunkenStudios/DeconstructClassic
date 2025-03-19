using DeconstructClassic.Memory;
using System.Numerics;

namespace DeconstructClassic.ConstructData.LevelBlock {
    public class LayerObject {
        private int _unknownKey;
        public Vector2 Position;
        public int Width;
        public int Height;
        public float Angle;
        public int Filter;

        public int ObjectID;
        public int InstanceID;

        public string[] PrivateValues;
        public byte[] ObjectData;

        public LayerObject(ByteReader reader) {
            _unknownKey = reader.ReadInt();
            Position = new Vector2(reader.ReadInt(), reader.ReadInt());
            Width = reader.ReadInt();
            Height = reader.ReadInt();
            Angle = reader.ReadFloat();
            Filter = reader.ReadInt();

            ObjectID = reader.ReadInt();
            InstanceID = reader.ReadInt();
            ObjectID = reader.ReadInt(); // Why tho?

            PrivateValues = new string[reader.ReadInt()];
            for (int i = 0; i < PrivateValues.Length; i++) {
                PrivateValues[i] = reader.ReadAutoAscii();
            }

            ObjectData = reader.ReadBytes(reader.ReadInt());
        }
    }
}
