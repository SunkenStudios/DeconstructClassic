using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.AppBlock
{
    public class Control
    {
        public string Name = string.Empty;
        public int VirtualKey;
        public int Player;

        public Control(ByteReader reader)
        {
            Name = reader.ReadAutoAscii();
            VirtualKey = reader.ReadInt();
            Player = reader.ReadInt();
        }
    }
}
