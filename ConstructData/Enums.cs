namespace DeconstructClassic.ConstructData
{
    public class Enums
    {
        public enum FPSMode
        {
            VSync,
            Unlimited,
            Fixed
        }

        public enum VariableType
        {
            Number,
            Text
        }

        public enum SimulateShaders
        {
            NoSimulation,
            PixelShader14,
            PixelShader11,
            PixelShader00
        }

        public enum TextRenderingMode
        {
            Aliased,
            AntiAliased,
            ClearType
        }

        public enum ResizeSetting
        {
            Disabled,
            ShowMore,
            Stretch
        }

        public enum LayoutTextureLoading
        {
            UseAppSetting,
            LoadOnAppStart,
            LoadOnLayoutStart
        }

        public enum LayerType
        {
            Normal,
            WindowControls,
            NonFrame,
            Include
        }

        public enum LayerSampler
        {
            Default,
            Point,
            Linear
        }

        public enum EventType
        {
            Event,
            Comment,
            Group,
            Include,
            Script
        }

        public enum EventCaps
        {
            None,
            BeginEventList,
            BeginEvent,
            BeginConditions,
            BeginCondition,
            EndCondition,
            EndConditions,
            BeginActions,
            BeginAction,
            EndAction,
            EndActions,
            BeginParam,
            EndParam,
            EndEvent,
            EndEventList,
            BeginGroup,
            EndGroup
        }

        public enum TokenType
        {
            Null,
            AnyBinaryOperator,
            AnyFunction,
            AnyValue,
            Integer,
            Float,
            StringLiteral,
            Identifier,
            Array,
            VariableName,
            LeftBracket,
            RightBracket,
            Comma,
            Dot,
            LeftCurly,
            RightCurly,
            At,
            Add,
            Subtract,
            Multiply,
            Divide,
            Mod,
            Sin,
            Cos,
            Tan,
            Sqrt,
            FuncStr,
            FuncInt,
            FuncFloat,
            Equal,
            Less,
            Greater,
            LessEqual,
            GreaterEqual,
            NotEqual,
            Conditional,
            Colon,
            And,
            Or,
            Asin,
            Acos,
            Atan,
            Abs,
            Exp,
            Ln,
            Log10,
            Floor,
            Ceil,
            Round,
            Random,
            Len,
            Whitespace,
            Color
        }
    }
}
