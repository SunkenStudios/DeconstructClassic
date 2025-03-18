using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.LevelBlock
{
    public class Behaviour
    {
        public int ObjectID;
        public int NewDLLIndex;
        public int BehaviourIndex;
        public string Name;
        public byte[] PropertiesData;

        public ACEEntry[] Actions = [];
        public ACEEntry[] Conditions = [];
        public ACEEntry[] Expressions = [];

        public Behaviour(ByteReader reader)
        {
            ObjectID = reader.ReadInt();
            NewDLLIndex = reader.ReadInt();
            BehaviourIndex = reader.ReadInt();
            Name = reader.ReadAutoAscii();
            PropertiesData = reader.ReadBytes(reader.ReadInt());

            if (reader.ReadBool())
            {
                Actions = new ACEEntry[reader.ReadInt()];
                for (int i = 0; i < Actions.Length; i++)
                    Actions[i] = new ACEEntry(reader);

                Conditions = new ACEEntry[reader.ReadInt()];
                for (int i = 0; i < Conditions.Length; i++)
                    Conditions[i] = new ACEEntry(reader);

                Expressions = new ACEEntry[reader.ReadInt()];
                for (int i = 0; i < Expressions.Length; i++)
                    Expressions[i] = new ACEEntry(reader);
            }
        }
    }
}
