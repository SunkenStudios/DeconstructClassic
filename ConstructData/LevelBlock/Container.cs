using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.LevelBlock
{
    public class Container
    {
        public int[] ObjectIDs;

        public Container(ByteReader reader)
        {
            ObjectIDs = new int[reader.ReadInt()];
            for (int i = 0; i < ObjectIDs.Length; i++)
                ObjectIDs[i] = reader.ReadInt();
        }
    }
}
