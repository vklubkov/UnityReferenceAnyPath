#if UNITY_EDITOR
namespace ReferenceAnyPath {
    public static class PathHelper {
        public static void OnBeforeSerialize(ref string relativePath,
            ref string assetPath, ref string runtimePath, ref bool error) => error = false;

        public static void OnBeforeSerialize(
            int width,
            int height,
            int bits,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) {
            var unpackedRelativePath = relativePath.UnpackPathSimple();
            error = unpackedRelativePath != null && (width <= 0 || height <= 0 || bits <= 0);
        }
    }
}
#endif