using System.IO;

namespace ReferenceAnyPath {
    public class RawHeightmapBinaryResourceFileValidator : BinaryResourceFileValidator, IRawHeightmapValidator {
        public bool IsValidHeightmapFile(string[] extensions, string absolutePath, int width, int height, int bits) {
            if (!base.IsValidPath(extensions, absolutePath))
                return false;

            var bytes = bits / 8;
            var expectedFileSize = width * height * bytes;
            var actualFileSize = new FileInfo(absolutePath).Length;
            return expectedFileSize == actualFileSize;
        }
    }
}