namespace DeconstructClassic.ConstructData {
    public class Enums {
        public enum FPSMode {
            VSync,
            Unlimited,
            Fixed
        }

        public enum VariableType {
            Number,
            Text
        }

        public enum SimulateShaders {
            NoSimulation,
            PixelShader14,
            PixelShader11,
            PixelShader00
        }

        public enum TextRenderingMode {
            Aliased,
            AntiAliased,
            ClearType
        }

        public enum ResizeSetting {
            Disabled,
            ShowMore,
            Stretch
        }

        public enum LayoutTextureLoading {
            UseAppSetting,
            LoadOnAppStart,
            LoadOnLayoutStart
        }

        public enum LayerType {
            Normal,
            WindowControls,
            NonFrame,
            Include
        }

        public enum LayerSampler {
            Default,
            Point,
            Linear
        }
    }
}
