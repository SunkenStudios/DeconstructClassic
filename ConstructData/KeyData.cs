using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData
{
    public class KeyData
    {
        public string Key;
        public Enums.VariableType Type;
        public object? Value;

        public KeyData(ByteReader reader)
        {
            Key = reader.ReadAutoAscii();
            Type = (Enums.VariableType)reader.ReadInt();
            switch (Type)
            {
                case Enums.VariableType.Number:
                    Value = reader.ReadInt();
                    break;
                case Enums.VariableType.Text:
                    Value = reader.ReadAutoAscii();
                    break;
            }
        }
    }
}
