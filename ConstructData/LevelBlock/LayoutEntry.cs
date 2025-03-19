using DeconstructClassic.Memory;
using System.Drawing;

namespace DeconstructClassic.ConstructData.LevelBlock {
    public class LayoutEntry {
        public int Width;
        public int Height;
        public string Name;
        public Color Color;
        public bool UnboundedScrolling;
        public bool ApplicationBackground;

        public KeyData[] KeyData;
        public Layer[] Layers;
        public int[] Images;

        public Enums.LayoutTextureLoading TextureLoading;

        public LayoutEntry(ByteReader reader) {
            Width = reader.ReadInt();
            Height = reader.ReadInt();
            Name = reader.ReadAutoAscii();
            Color = reader.ReadColor();
            UnboundedScrolling = reader.ReadBool();
            ApplicationBackground = reader.ReadBool();

            KeyData = new KeyData[reader.ReadInt()];
            for (int i = 0; i < KeyData.Length; i++) {
                KeyData[i] = new KeyData(reader);
            }

            Layers = new Layer[reader.ReadInt()];
            for (int i = 0; i < Layers.Length; i++) {
                Layers[i] = new Layer(reader);
            }

            Images = new int[reader.ReadInt()];
            for (int i = 0; i < Images.Length; i++) {
                Images[i] = reader.ReadInt();
            }

            TextureLoading = (Enums.LayoutTextureLoading)reader.ReadInt();
        }
    }
}
