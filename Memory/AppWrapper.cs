using DeconstructClassic.ConstructData.AppBlock;
using DeconstructClassic.ConstructData.HlslBlock;
using DeconstructClassic.ConstructData.ImageBlock;
using DeconstructClassic.ConstructData.LevelBlock;
using System.Collections.Generic;

namespace DeconstructClassic.Memory {
    public class AppWrapper {
        public AppData? AppData { get; set; }
        public ImageBank? ImageBank { get; set; }
        public LayoutBank? LayoutBank { get; set; }
        public ShaderBank? ShaderBank { get; set; }
        public List<BinaryFile> BinaryFiles { get; set; } = [];
        public List<DLLFileInfo> DLLFileInfos { get; set; } = [];
    }
}
