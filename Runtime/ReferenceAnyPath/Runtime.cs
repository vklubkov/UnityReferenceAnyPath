using System;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace ReferenceAnyPath {
    [Serializable]
    public class RuntimeAsset : AnyBase, IRuntimeAsset {
#if UNITY_EDITOR
        [SerializeField] UnityObject _object;

        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            RuntimeAssetHelper.OnBeforeSerialize(
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
    public class Runtime<T> : FileBase, IRuntimeFile where T : UnityObject {
#if UNITY_EDITOR
        [SerializeField] T _object;

        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            RuntimeAssetHelper.OnBeforeSerialize(
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
    public class RuntimeFile : FileBase, IRuntimeFile {
#if UNITY_EDITOR
        [SerializeField] UnityObject _object;

        protected override void OnBeforeSerialize(ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            RuntimeAssetHelper.OnBeforeSerialize(
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
    public class RuntimeFolder : FolderBase, IRuntimeFolder {
#if UNITY_EDITOR
        [SerializeField] UnityObject _object;

        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            RuntimeAssetHelper.OnBeforeSerialize(
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