#if UNITY_EDITOR
using System;
using UnityObject = UnityEngine.Object;

namespace ReferenceAnyPath {
    public static class RuntimeAssetHelper {
        public static void OnBeforeSerialize(
            UnityObject @object,
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error,
            Func<string, string> getRuntimePath) {
            AssetHelper.OnBeforeSerialize(
                @object,
                ref path,
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath);

#if !REFERENCE_ANY_PATH_NO_VALIDATION_BEFORE_SERIALIZATION
            HandleRuntimePath(relativePath, runtimePath, ref error);
#endif
        }

        public static void OnBeforeSerialize(
            UnityObject @object,
            int width,
            int height,
            int bits,
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error,
            Func<string, string> getRuntimePath) {
            AssetHelper.OnBeforeSerialize(
                @object,
                ref path,
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                GetRuntimePath);

            string GetRuntimePath(string unpackedAssetPath) {
                if (width <= 0 || height <= 0 || bits <= 0)
                    return null;

                return getRuntimePath.Invoke(unpackedAssetPath);
            }

#if !REFERENCE_ANY_PATH_NO_VALIDATION_BEFORE_SERIALIZATION
            HandleRuntimePath(relativePath, runtimePath, ref error);
#endif

            var unpackedRelativePath = relativePath.UnpackPathComplex();
            if (unpackedRelativePath != null && @object != null && (width <= 0 || height <= 0 || bits <= 0)) {
                error = true;
            }
        }

#if !REFERENCE_ANY_PATH_NO_VALIDATION_BEFORE_SERIALIZATION
        static void HandleRuntimePath(string relativePath, string runtimePath, ref bool error) {
            var unpackedRelativePath = relativePath.UnpackPathComplex();
            var unpackedRuntimePath = runtimePath.UnpackPathComplex();
            if (unpackedRelativePath == null || unpackedRuntimePath != null)
                return;

            error = true;
        }
#endif
    }
}
#endif