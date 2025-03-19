using DeconstructClassic.Memory;
using System.Drawing;
using System.Numerics;

namespace DeconstructClassic.ConstructData.LevelBlock {
    public class Layer {
        public int ID;
        public string Name;
        public Enums.LayerType Type;
        public Color ColorFilter;
        public float Opacity;
        public float Angle;
        public Vector2 ScrollCoefficient;
        public Vector2 ScrollOffset;

        public Vector2 ZoomOffset;
        public Vector2 ZoomCoefficient;

        public bool ClearBackground;
        public Color BackgroundColor;
        public bool ForceOwnTexture;
        public Enums.LayerSampler Sampler;
        public bool Enable3DLayering;
        public bool ClearDepthBuffer;

        public LayerObject[] LayerObjects;

        public Layer(ByteReader reader) {
            ID = reader.ReadInt();
            Name = reader.ReadAutoAscii();
            Type = (Enums.LayerType)reader.ReadByte();
            ColorFilter = reader.ReadColor();
            Opacity = reader.ReadFloat();
            Angle = reader.ReadFloat();
            ScrollCoefficient = new Vector2(reader.ReadFloat(), reader.ReadFloat());
            ScrollOffset = new Vector2(reader.ReadFloat(), reader.ReadFloat());

            ZoomOffset = new Vector2(reader.ReadFloat(), reader.ReadFloat());
            ZoomCoefficient = new Vector2(reader.ReadFloat(), reader.ReadFloat());

            ClearBackground = reader.ReadBool();
            BackgroundColor = reader.ReadColor();
            ForceOwnTexture = reader.ReadBool();
            Sampler = (Enums.LayerSampler)reader.ReadInt();
            Enable3DLayering = reader.ReadBool();
            ClearDepthBuffer = reader.ReadBool();

            LayerObjects = new LayerObject[reader.ReadInt()];
            for (int i = 0; i < LayerObjects.Length; i++) {
                LayerObjects[i] = new LayerObject(reader);
            }
        }
    }
}
