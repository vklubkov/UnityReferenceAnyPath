#if UNITY_EDITOR
using System;
using UnityObject = UnityEngine.Object;

#if !REFERENCE_ANY_PATH_NO_VALIDATION_BEFORE_SERIALIZATION
using UnityEditor;
#endif

namespace ReferenceAnyPath {
    public static class AssetHelper {
        public static void OnBeforeSerialize(
            UnityObject @object,
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error,
            Func<string, string> getRuntimePath) {
            PathHelper.OnBeforeSerialize(
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error);

#if !REFERENCE_ANY_PATH_NO_VALIDATION_BEFORE_SERIALIZATION
            HandleMoveOrDelete(
                @object,
                ref path,
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath);
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
            PathHelper.OnBeforeSerialize(
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error);

#if !REFERENCE_ANY_PATH_NO_VALIDATION_BEFORE_SERIALIZATION
            HandleMoveOrDelete(
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
#endif

            var unpackedRelativePath = relativePath.UnpackPathSimple();
            if (unpackedRelativePath != null && @object != null && (width <= 0 || height <= 0 || bits <= 0)) {
                error = true;
            }
        }

#if !REFERENCE_ANY_PATH_NO_VALIDATION_BEFORE_SERIALIZATION
        static void HandleMoveOrDelete(UnityObject @object,
            ref string path, ref string relativePath, ref string assetPath,
            ref string runtimePath, ref bool error, Func<string, string> getRuntimePath) {
            var unpackedPath = path.UnpackPathComplex();
            if (string.IsNullOrEmpty(unpackedPath))
                return; // Not set

            var unpackedAssetPath = assetPath.UnpackPathSimple();
            if (string.IsNullOrEmpty(unpackedAssetPath))
                return; // Not an asset

            // Calling AssetDatabase.GetAssetPath() here results in
            // "Objects are trying to be loaded during a domain backup.
            // This is not allowed as it will lead to undefined behaviour!"
            // error. AssetDatabase.TryGetGUIDAndLocalFileIdentifier() +
            // AssetDatabase.GUIDToAssetPath() seem to not trigger the
            // error. At the same time, a missing asset results in a valid
            // path this way. So we need to check the object for null first.
            if (@object == null) {
                runtimePath = string.Empty;
                error = true;
                return;  // Object is missing
            }

            if (!AssetDatabase.TryGetGUIDAndLocalFileIdentifier(@object, out var guid, out long _)) {
                runtimePath = string.Empty;
                error = true;
                return;  // Asset not found
            }

            var newAssetPath = AssetDatabase.GUIDToAssetPath(guid);
            if (newAssetPath == assetPath) {
                RestoreRuntimePathIfNeeded(newAssetPath, ref runtimePath, getRuntimePath);
                return; // Asset path is the same
            }

            if (string.IsNullOrEmpty(newAssetPath)) {
                runtimePath = string.Empty;
                error = true;
                return; // Should not happen probably
            }

            // Asset was moved
            var newRelativePath = newAssetPath.GetRelativePathFromAssetPath();
            path = relativePath = newRelativePath.PackPathComplex();
            assetPath = newAssetPath.PackPathSimple();
            runtimePath = getRuntimePath.Invoke(newAssetPath).PackPathComplex();
        }

        static void RestoreRuntimePathIfNeeded(
            string newAssetPath, ref string runtimePath, Func<string, string> getRuntimePath) {
            var unpackedRuntimePath = runtimePath.UnpackPathComplex();
            if (unpackedRuntimePath != null)
                return;

            runtimePath = getRuntimePath.Invoke(newAssetPath).PackPathComplex();
        }
#endif
    }
}
#endif