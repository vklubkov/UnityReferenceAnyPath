#if UNITY_EDITOR
using System;
using UnityObject = UnityEngine.Object;

namespace ReferenceAnyPath {
    public static class SceneHelper {
        public static void OnBeforeSerialize(
            UnityObject @object,
            ref string path,
            ref string relativePath,
            ref string absolutePath,
            ref string assetPath,
            ref string runtimePath,
            ref string name,
            ref bool error,
            Func<string, string> getRuntimePath) {
            RuntimeAssetHelper.OnBeforeSerialize(
                @object,
                ref path,
                ref relativePath,
                ref absolutePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath);

            name = runtimePath.UnpackPathComplex().GetFileName();
        }
    }
}
#endif