using DeconstructClassic.Memory;
using System.Linq;

namespace DeconstructClassic.ConstructData.ImageBlock {
    public class ImageBank {
        public ImageEntry[] Images;

        public ImageBank(ByteReader reader) {
            Images = new ImageEntry[reader.ReadInt()];
            for (int i = 0; i < Images.Length; i++) {
                Images[i] = new ImageEntry(reader);
            }
        }

        public ImageEntry? GetImage(int id) {
            return Images.FirstOrDefault(x => x.ID == id);
        }
    }
}
