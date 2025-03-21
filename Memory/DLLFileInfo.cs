using Avalonia;
using Ressy;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DeconstructClassic.Memory {
    public class DLLFileInfo {
        public string ObjectName = string.Empty;
        public string Author = string.Empty;
        public string Version = string.Empty;
        public string Description = string.Empty;
        public string BuildDate = string.Empty; // Unsure if this is actually a build date
        public string Copyright = string.Empty;
        public string Website = string.Empty;
        public string Email = string.Empty;
        public string Category = string.Empty;
        public string ReleaseType = string.Empty;

        public DLLFileInfo(PortableExecutable PE) {
            var stringTable = PE.GetResource(new ResourceIdentifier(
                ResourceType.String, ResourceName.FromCode(1)));

            ByteReader reader = new ByteReader(stringTable.Data);
            reader.SetUnicode(reader.PeekUShort() == 0);
            List<string> stringTableData = [];
            while (reader.Tell() < reader.Size()) {
                ushort strLength = reader.IsUnicode() ? reader.ReadUShort() : reader.ReadByte();
                if (strLength == 0) {
                    continue;
                }
                string str = reader.ReadYuniversal(strLength);
                stringTableData.Add(str);
            }

            if (stringTableData.Count < 10) {
                throw new InvalidDataException("Expected 10 String Table inputs from DLL, got " + stringTableData.Count);
            }

            ObjectName = stringTableData[0];
            Author = stringTableData[1];
            Version = stringTableData[2];
            Description = stringTableData[3];
            BuildDate = stringTableData[4];
            Copyright = stringTableData[5];
            Website = stringTableData[6];
            Email = stringTableData[7];
            Category = stringTableData[8];
            ReleaseType = stringTableData[9];
        }
    }
}
