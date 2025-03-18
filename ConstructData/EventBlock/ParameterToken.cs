using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.EventBlock
{
    public class ParameterToken
    {
        public Enums.TokenType Type;
        public object? Value;

        public ParameterToken(ByteReader reader)
        {
            Type = (Enums.TokenType)reader.ReadInt();
            switch (Type)
            {
                case Enums.TokenType.Integer:
                case Enums.TokenType.Color:
                    Value = reader.ReadLong();
                    break;
                case Enums.TokenType.Float:
                    Value = reader.ReadDouble();
                    break;
                case Enums.TokenType.StringLiteral:
                case Enums.TokenType.Identifier:
                case Enums.TokenType.VariableName:
                    Value = reader.ReadAutoAscii();
                    break;
            }
        }
    }
}
