using System;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace ReferenceAnyPath {
    [Serializable]
    public class Asset : AnyBase, IAsset {
#if UNITY_EDITOR
        [SerializeField] UnityObject _object;

        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            AssetHelper.OnBeforeSerialize(
                _object,
                ref path,
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath: StringExtensions.GetRuntimePathFromAssetPath);
#endif
    }

    [Serializable]
    public class Asset<T> : FileBase, IAssetFile where T : UnityObject {
#if UNITY_EDITOR
        [SerializeField] T _object;

        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            AssetHelper.OnBeforeSerialize(
                _object,
                ref path,
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath: StringExtensions.GetRuntimePathFromAssetPath);
#endif
    }

    [Serializable]
    public class AssetFile : FileBase, IAssetFile {
#if UNITY_EDITOR
        [SerializeField] UnityObject _object;

        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            AssetHelper.OnBeforeSerialize(
                _object,
                ref path,
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath: StringExtensions.GetRuntimePathFromAssetPath);
#endif
    }

    [Serializable]
    public class AssetFolder : FolderBase, IAssetFolder {
#if UNITY_EDITOR
        [SerializeField] UnityObject _object;

        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            AssetHelper.OnBeforeSerialize(
                _object,
                ref path,
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath: StringExtensions.GetRuntimePathFromAssetPath);
#endif
    }
}