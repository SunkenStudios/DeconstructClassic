using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.ImageBlock
{
    public class ActionPoint
    {
        public int X;
        public int Y;
        public string Name;

        public ActionPoint(ByteReader reader)
        {
            X = reader.ReadInt();
            Y = reader.ReadInt();
            Name = reader.ReadAutoAscii();
        }
    }
}
