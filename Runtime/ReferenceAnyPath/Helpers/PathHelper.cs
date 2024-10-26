#if UNITY_EDITOR
namespace ReferenceAnyPath {
    public static class PathHelper {
        public static void OnBeforeSerialize(ref string relativePath, ref string absolutePath,
            ref string assetPath, ref string runtimePath, ref bool error) => error = false;

        public static void OnBeforeSerialize(
            int width,
            int height,
            int bits,
            ref string relativePath,
            ref string absolutePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) {
            var unpackedAbsolutePath = absolutePath.UnpackPathSimple();
            error = unpackedAbsolutePath != null && (width <= 0 || height <= 0 || bits <= 0);
        }
    }
}
#endif