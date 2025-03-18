using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.EventBlock
{
    public abstract class EVT_Base
    {
        public abstract EVT_Base Read(ByteReader reader);
    }
}
