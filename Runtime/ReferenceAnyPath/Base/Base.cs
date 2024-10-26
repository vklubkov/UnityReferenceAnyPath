#if UNITY_EDITOR
using System;
using UnityEngine;

namespace ReferenceAnyPath {
    [Serializable]
    public abstract class Base : IPath, ISerializationCallbackReceiver {
        [SerializeField] string _path = string.Empty;
        [SerializeField] string _relativePath;
        [SerializeField] string _absolutePath;
        [SerializeField] string _assetPath;
        [SerializeField] string _runtimePath;
        [SerializeField] bool _error;

        public abstract string Path { get; }
        public string PathUnsafe => _runtimePath;

        public abstract string RelativePath { get; }
        public string RelativePathUnsafe => _relativePath;

        public abstract string AbsolutePath { get; }
        public string AbsolutePathUnsafe => _absolutePath;

        public abstract string AssetPath { get; }
        public string AssetPathUnsafe => _assetPath;

        public abstract string RuntimePath { get; }
        public string RuntimePathUnsafe => _runtimePath;

        void ISerializationCallbackReceiver.OnBeforeSerialize() => OnBeforeSerialize(
            ref _path, ref _relativePath, ref _absolutePath, ref _assetPath, ref _runtimePath, ref _error);

        protected abstract void OnBeforeSerialize(
            ref string path, ref string relativePath, ref string absolutePath,
            ref string assetPath, ref string runtimePath, ref bool error);

        void ISerializationCallbackReceiver.OnAfterDeserialize() { }
    }
}
#else
using System;
using UnityEngine;

namespace ReferenceAnyPath {
    [Serializable]
    public abstract class Base : IPath {
        [SerializeField] string _runtimePath;
        public abstract string Path { get; }
        public string PathUnsafe => _runtimePath;
    }
}
#endif