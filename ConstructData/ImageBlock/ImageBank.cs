using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.ImageBlock
{
    public class ImageBank
    {
        public ImageEntry[] Images;

        public ImageBank(ByteReader reader)
        {
            Images = new ImageEntry[reader.ReadInt()];
            for (int i = 0; i < Images.Length; i++)
                Images[i] = new ImageEntry(reader);
        }
    }
}
