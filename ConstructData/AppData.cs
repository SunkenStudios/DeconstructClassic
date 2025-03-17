using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData
{
    public class AppData
    {
        public string AppName = string.Empty;
        public int WindowWidth;
        public int WindowHeight;
        public int EyeDistance;

        public int ShowMenu;
        public bool IsScreensaver;

        public Enums.FPSMode FPSMode;
        public int FPS;

        public bool Fullscreen;
        public int Sampler;

        public GlobalVariable[] GlobalVariables;

        public AppData(ByteReader reader)
        {
            AppName = reader.ReadAutoAscii();
            WindowWidth = reader.ReadInt();
            WindowHeight = reader.ReadInt();
            EyeDistance = reader.ReadInt();

            ShowMenu = reader.ReadInt();
            IsScreensaver = reader.ReadBool();

            FPSMode = (Enums.FPSMode)reader.ReadByte();
            FPS = reader.ReadInt();

            Fullscreen = reader.ReadBool();
            Sampler = reader.ReadInt();

            GlobalVariables = new GlobalVariable[reader.ReadInt()];
            for (int i = 0; i < GlobalVariables.Length; i++)
                GlobalVariables[i] = new GlobalVariable(reader);
        }
    }
}
