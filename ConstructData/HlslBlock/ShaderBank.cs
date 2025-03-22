using DeconstructClassic.Memory;

namespace DeconstructClassic.ConstructData.HlslBlock {
    public class ShaderBank {
        public bool CanRunWithoutPixelShaders;
        public ShaderEntry[] ShaderEntries;
        public ShaderObject[] ShaderObjects;
        public ShaderLayer[] ShaderLayers;
        public ShaderTransition[] ShaderTransitions;

        public ShaderBank(ByteReader reader) {
            CanRunWithoutPixelShaders = reader.ReadInt() != 0;

            ShaderEntries = new ShaderEntry[reader.ReadInt()];
            for (int i = 0; i < ShaderEntries.Length; i++) {
                ShaderEntries[i] = new ShaderEntry(reader);
            }

            ShaderObjects = new ShaderObject[reader.ReadInt()];
            for (int i = 0; i < ShaderObjects.Length; i++) {
                ShaderObjects[i] = new ShaderObject(reader);
            }

            ShaderLayers = new ShaderLayer[reader.ReadInt()];
            for (int i = 0; i < ShaderLayers.Length; i++) {
                ShaderLayers[i] = new ShaderLayer(reader);
            }

            ShaderTransitions = new ShaderTransition[reader.ReadInt()];
            for (int i = 0; i < ShaderTransitions.Length; i++) {
                ShaderTransitions[i] = new ShaderTransition(reader);
            }
        }
    }
}
