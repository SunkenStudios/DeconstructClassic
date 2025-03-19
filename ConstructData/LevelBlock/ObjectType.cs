using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.LevelBlock {
    public class ObjectType {
        public int ID;
        public string Name;
        public int DLLIndex;

        public bool Global;
        public int DestroyWhen;

        public PrivateVariable[] PrivateVariables;

        public ACEEntry[] Actions = [];
        public ACEEntry[] Conditions = [];
        public ACEEntry[] Expressions = [];

        public ObjectType(ByteReader reader) {
            ID = reader.ReadInt();
            Name = reader.ReadAutoAscii();
            DLLIndex = reader.ReadInt();

            Global = reader.ReadBool();
            DestroyWhen = reader.ReadInt();

            PrivateVariables = new PrivateVariable[reader.ReadInt()];
            for (int i = 0; i < PrivateVariables.Length; i++) {
                PrivateVariables[i] = new PrivateVariable(reader);
            }

            reader.Skip(16); // Padding

            if (reader.ReadBool()) {
                Actions = new ACEEntry[reader.ReadInt()];
                for (int i = 0; i < Actions.Length; i++) {
                    Actions[i] = new ACEEntry(reader);
                }

                Conditions = new ACEEntry[reader.ReadInt()];
                for (int i = 0; i < Conditions.Length; i++) {
                    Conditions[i] = new ACEEntry(reader);
                }

                Expressions = new ACEEntry[reader.ReadInt()];
                for (int i = 0; i < Expressions.Length; i++) {
                    Expressions[i] = new ACEEntry(reader);
                }
            }
        }
    }
}
