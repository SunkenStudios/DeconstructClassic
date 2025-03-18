using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData
{
    public class AppData
    {
        public string AppName;
        public int WindowWidth;
        public int WindowHeight;
        public float EyeDistance;

        public int ShowMenu;
        public bool IsScreensaver;

        public Enums.FPSMode FPSMode;
        public int FPS;

        public bool Fullscreen;
        public int Sampler;

        public GlobalVariable[] GlobalVariables;
        public Control[] Controls;

        public bool DisableWindowsKey;

        public KeyData[] KeyData;

        public Enums.SimulateShaders SimulateShaders;

        public string FilePath;
        public int FPSInCaption;
        public bool UseMotionBlur;
        public int MotionBlurSteps;

        public Enums.TextRenderingMode TextRenderingMode;

        public bool OverrideTimeDelta;
        public float TimeDeltaOverride;

        public bool Caption;
        public bool MinimizeBox;
        public Enums.ResizeSetting ResizeSetting;
        public bool MaximizeBox;

        public float MinimumFPS;
        public int LayoutID;
        public uint MultiSamples;
        public Enums.LayoutTextureLoading TextureLoading;

        public AppData(ByteReader reader)
        {
            AppName = reader.ReadAutoAscii();
            WindowWidth = reader.ReadInt();
            WindowHeight = reader.ReadInt();
            EyeDistance = reader.ReadFloat();

            ShowMenu = reader.ReadInt();
            IsScreensaver = reader.ReadBool();

            FPSMode = (Enums.FPSMode)reader.ReadByte();
            FPS = reader.ReadInt();

            Fullscreen = reader.ReadBool();
            Sampler = reader.ReadInt();

            GlobalVariables = new GlobalVariable[reader.ReadInt()];
            for (int i = 0; i < GlobalVariables.Length; i++)
                GlobalVariables[i] = new GlobalVariable(reader);

            Controls = new Control[reader.ReadInt()];
            for (int i = 0; i < Controls.Length; i++)
                Controls[i] = new Control(reader);

            DisableWindowsKey = reader.ReadBool();

            KeyData = new KeyData[reader.ReadInt()];
            for (int i = 0; i < KeyData.Length; i++)
                KeyData[i] = new KeyData(reader);

            SimulateShaders = (Enums.SimulateShaders)reader.ReadInt();

            FilePath = reader.ReadAutoAscii();
            FPSInCaption = reader.ReadInt();
            UseMotionBlur = reader.ReadBool();
            MotionBlurSteps = reader.ReadInt();

            TextRenderingMode = (Enums.TextRenderingMode)reader.ReadInt();

            OverrideTimeDelta = reader.ReadBool();
            TimeDeltaOverride = reader.ReadFloat();

            Caption = reader.ReadBool();
            MinimizeBox = reader.ReadBool();
            ResizeSetting = (Enums.ResizeSetting)reader.ReadInt();
            MaximizeBox = reader.ReadBool();

            MinimumFPS = reader.ReadFloat();
            LayoutID = reader.ReadInt();
            MultiSamples = reader.ReadUInt();
            TextureLoading = (Enums.LayoutTextureLoading)reader.ReadInt();
        }
    }
}
