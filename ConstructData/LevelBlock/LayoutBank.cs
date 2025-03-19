using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.LevelBlock {
    public class LayoutBank {
        public ObjectType[] ObjectTypes;
        public Behaviour[] Behaviours;
        public Trait[] Traits;
        public Family[] Families;
        public Container[] Containers;
        public LayoutEntry[] Layouts;
        public Animation[] Animations;

        public LayoutBank(ByteReader reader) {
            ObjectTypes = new ObjectType[reader.ReadInt()];
            for (int i = 0; i < ObjectTypes.Length; i++) {
                ObjectTypes[i] = new ObjectType(reader);
            }

            Behaviours = new Behaviour[reader.ReadInt()];
            for (int i = 0; i < Behaviours.Length; i++) {
                Behaviours[i] = new Behaviour(reader);
            }

            Traits = new Trait[reader.ReadInt()];
            for (int i = 0; i < Traits.Length; i++) {
                Traits[i] = new Trait(reader);
            }

            Families = new Family[reader.ReadInt()];
            for (int i = 0; i < Families.Length; i++) {
                Families[i] = new Family(reader);
            }

            Containers = new Container[reader.ReadInt()];
            for (int i = 0; i < Containers.Length; i++) {
                Containers[i] = new Container(reader);
            }

            Layouts = new LayoutEntry[reader.ReadInt()];
            for (int i = 0; i < Layouts.Length; i++) {
                Layouts[i] = new LayoutEntry(reader);
            }

            Animations = new Animation[reader.ReadInt()];
            for (int i = 0; i < Animations.Length; i++) {
                Animations[i] = new Animation(reader);
            }
        }
    }
}
