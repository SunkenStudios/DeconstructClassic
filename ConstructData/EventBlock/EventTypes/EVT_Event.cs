using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.EventBlock.EventTypes
{
    public class EVT_Event : EVT_Base
    {
        public int LineNumber;
        public int SheetNumber;

        public override EVT_Base Read(ByteReader reader)
        {
            LineNumber = reader.ReadInt();
            SheetNumber = reader.ReadInt();

            Enums.EventCaps EventCap = Enums.EventCaps.None;
            while (EventCap != Enums.EventCaps.EndEvent)
            {
                EventCap = (Enums.EventCaps)reader.ReadByte();
                switch (EventCap)
                {
                    case Enums.EventCaps.BeginConditions:
                        break;
                }
            }

            return this;
        }
    }
}
