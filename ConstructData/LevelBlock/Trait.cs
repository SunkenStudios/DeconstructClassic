using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.LevelBlock
{
    public class Trait
    {
        public string Name;
        public int[] ObjectIDs;

        public Trait(ByteReader reader)
        {
            Name = reader.ReadAutoAscii();
            ObjectIDs = new int[reader.ReadInt()];
            for (int i = 0; i < ObjectIDs.Length; i++)
                ObjectIDs[i] = reader.ReadInt();
        }
    }
}
