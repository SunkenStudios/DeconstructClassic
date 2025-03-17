using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData
{
    public class GlobalVariable
    {
        public string Name = string.Empty;
        public Enums.VariableType Type;
        public object? Value;

        public GlobalVariable(ByteReader reader)
        {
            Name = reader.ReadAutoAscii();
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
