using DeconstructClassic.Memory;
using System.Drawing;

namespace DeconstructClassic.ConstructData.ObjectTypes {
    public class CText {
        public int Version;

        public string Text = string.Empty;
        public string FontName = string.Empty;
        public int FontSize;
        public bool Bold;
        public bool Italic;
        public Color TextColor;
        public float Opacity;
        public Enums.TextHorizontalAlignment HorizontalAlignment;
        public Enums.TextVerticalAlignment VerticalAlignment;
        public bool HideAtStart;

        public CText(ByteReader reader) {
            Version = reader.ReadInt();

            Text = reader.ReadAutoAscii();
            FontName = reader.ReadAutoAscii();
            FontSize = reader.ReadInt();
            Italic = reader.ReadInt() != 0;
            Bold = reader.ReadInt() != 0;
            TextColor = reader.ReadColor();
            Opacity = reader.ReadFloat();
            HorizontalAlignment = (Enums.TextHorizontalAlignment)reader.ReadInt();
            VerticalAlignment = (Enums.TextVerticalAlignment)reader.ReadInt();
            HideAtStart = reader.ReadBool();
        }
    }
}
