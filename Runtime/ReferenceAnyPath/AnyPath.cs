using System;

namespace ReferenceAnyPath {
    [Serializable]
    public class AnyPath : AnyBase, IAnyPath {
#if UNITY_EDITOR
        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            PathHelper.OnBeforeSerialize(
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error);
#endif
    }

    [Serializable]
    public class AnyFile : FileBase, IAnyFile {
#if UNITY_EDITOR
        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            PathHelper.OnBeforeSerialize(
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error);
#endif
    }

    [Serializable]
    public class AnyFolder : FolderBase, IAnyFolder {
#if UNITY_EDITOR
        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            PathHelper.OnBeforeSerialize(
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error);
#endif
    }
}