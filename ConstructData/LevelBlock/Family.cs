using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.LevelBlock {
    public class Family {
        public string Name;
        public int[] ObjectIDs;
        public PrivateVariable[] ReferenceVariables;

        public Family(ByteReader reader) {
            Name = reader.ReadAutoAscii();

            ObjectIDs = new int[reader.ReadInt()];
            for (int i = 0; i < ObjectIDs.Length; i++) {
                ObjectIDs[i] = reader.ReadInt();
            }

            ReferenceVariables = new PrivateVariable[reader.ReadInt()];
            for (int i = 0; i < ReferenceVariables.Length; i++) {
                ReferenceVariables[i] = new PrivateVariable(reader);
            }
        }
    }
}
