namespace ReferenceAnyPath {
    public interface IRawHeightmapValidator {
        public bool IsValidHeightmapFile(string[] extensions, string absolutePath, int width, int height, int bits);
    }
}