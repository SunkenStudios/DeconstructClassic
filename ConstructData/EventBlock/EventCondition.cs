using DeconstructClassic.Memory;
using System.Diagnostics;

namespace DeconstructClassic.ConstructData.EventBlock
{
    public class EventCondition
    {
        public int ObjectID;
        public int ID;
        public bool Negated;
        public int MID;
        public PARAM_Base[] Parameters;

        public EventCondition(ByteReader reader)
        {
            Debug.Assert((Enums.EventCaps)reader.ReadByte() == Enums.EventCaps.BeginCondition);

            ObjectID = reader.ReadInt();
            ID = reader.ReadInt();
            Negated = reader.ReadBool();
            MID = reader.ReadInt();

            Parameters = new PARAM_Base[reader.ReadInt()];
            for (int i = 0; i < Parameters.Length; i++)
            {
                ParameterToken[] tokens = new ParameterToken[reader.ReadInt()];
                for (int ii = 0; ii < tokens.Length; ii++)
                    tokens[ii] = new ParameterToken(reader);
            }

            Debug.Assert((Enums.EventCaps)reader.ReadByte() == Enums.EventCaps.EndCondition);
        }
    }
}
