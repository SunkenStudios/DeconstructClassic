using DeconstructClassic.ConstructData.EventBlock.EventTypes;
using DeconstructClassic.Memory;
using System.Collections.Generic;

namespace DeconstructClassic.ConstructData.EventBlock
{
    public class EventBank
    {
        public string[] EventSheetNames;
        public List<EVT_Base>[] EventBlocks;

        public EventBank(ByteReader reader)
        {
            EventSheetNames = new string[reader.ReadInt()];
            for (int i = 0; i < EventSheetNames.Length; i++)
                EventSheetNames[i] = reader.ReadAutoAscii();

            EventBlocks = new List<EVT_Base>[reader.ReadInt()];
            for (int i = 0; i < EventBlocks.Length; i++)
            {
                EventBlocks[i] = new List<EVT_Base>();
                Enums.EventCaps EventCap = Enums.EventCaps.None;
                while (EventCap != Enums.EventCaps.EndEventList)
                {
                    EventCap = (Enums.EventCaps)reader.ReadByte();
                    switch (EventCap)
                    {
                        case Enums.EventCaps.BeginEvent:
                            EventBlocks[i].Add(new EVT_Event().Read(reader));
                            break;
                    }
                }
            }
        }
    }
}
