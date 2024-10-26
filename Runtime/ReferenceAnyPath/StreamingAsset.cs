using System;
using System.Threading;
using UnityEngine;
using UnityObject = UnityEngine.Object;

#if REFERENCE_ANY_PATH_FORCE_UNITASK
using Cysharp.Threading.Tasks;
#endif

#if !UNITY_2023_1_OR_NEWER
using System.Threading.Tasks;
#endif

namespace ReferenceAnyPath {
    [Serializable]
    public class StreamingAsset : AnyBase, IStreamingAsset {
#if UNITY_EDITOR
        [SerializeField] UnityObject _object;
#endif

        string _streamingAssetPath;
        public string StreamingAssetPath => _streamingAssetPath ??= Path.GetStreamingAssetPath();

#if UNITY_EDITOR
        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string absolutePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            StreamingAssetHelper.OnBeforeSerialize(
                _object,
                ref path,
                ref relativePath,
                ref absolutePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath: StringExtensions.GetStreamingAssetPathFromAssetPath);
#endif
    }

    [Serializable]
    public class TextStreamingAsset : FileBase, ITextStreamingAssetFile {
#if UNITY_EDITOR
        [SerializeField] UnityObject _object;
#endif

        string _streamingAssetsPath;
        public string StreamingAssetPath => _streamingAssetsPath ??= Path.GetStreamingAssetPath();

#if UNITY_EDITOR
        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string absolutePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            StreamingAssetHelper.OnBeforeSerialize(
                _object,
                ref path,
                ref relativePath,
                ref absolutePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath: StringExtensions.GetStreamingAssetPathFromAssetPath);
#endif

#if REFERENCE_ANY_PATH_FORCE_UNITASK
        public UniTask<string> ReadAllTextAsync() => ReadAllTextAsync(default);

        public UniTask<string> ReadAllTextAsync(CancellationToken cancellationToken) =>
            StreamingAssetHelper.ReadAllTextAsync(StreamingAssetPath, cancellationToken);
#elif UNITY_2023_1_OR_NEWER
        public Awaitable<string> ReadAllTextAsync() => ReadAllTextAsync(default);

        public Awaitable<string> ReadAllTextAsync(CancellationToken cancellationToken) =>
            StreamingAssetHelper.ReadAllTextAsync(StreamingAssetPath, cancellationToken);
#else
        public Task<string> ReadAllTextAsync() => ReadAllTextAsync(default);

        public Task<string> ReadAllTextAsync(CancellationToken cancellationToken) =>
            StreamingAssetHelper.ReadAllTextAsync(StreamingAssetPath, cancellationToken);
#endif
    }

    [Serializable]
    public class BinaryStreamingAsset : FileBase, IBinaryStreamingAssetFile {
#if UNITY_EDITOR
        [SerializeField] UnityObject _object;
#endif

        string _streamingAssetsPath;
        public string StreamingAssetPath => _streamingAssetsPath ??= Path.GetStreamingAssetPath();

#if UNITY_EDITOR
        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string absolutePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            StreamingAssetHelper.OnBeforeSerialize(
                _object,
                ref path,
                ref relativePath,
                ref absolutePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath: StringExtensions.GetStreamingAssetPathFromAssetPath);
#endif

#if REFERENCE_ANY_PATH_FORCE_UNITASK
        public UniTask<byte[]> ReadAllBytesAsync() => ReadAllBytesAsync(default);

        public UniTask<byte[]> ReadAllBytesAsync(CancellationToken cancellationToken) =>
            StreamingAssetHelper.ReadAllBytesAsync(StreamingAssetPath, cancellationToken);
#elif UNITY_2023_1_OR_NEWER
        public Awaitable<byte[]> ReadAllBytesAsync() => ReadAllBytesAsync(default);

        public Awaitable<byte[]> ReadAllBytesAsync(CancellationToken cancellationToken) =>
            StreamingAssetHelper.ReadAllBytesAsync(StreamingAssetPath, cancellationToken);
#else
        public Task<byte[]> ReadAllBytesAsync() => ReadAllBytesAsync(default);

        public Task<byte[]> ReadAllBytesAsync(CancellationToken cancellationToken) =>
            StreamingAssetHelper.ReadAllBytesAsync(StreamingAssetPath, cancellationToken);
#endif
    }

    [Serializable]
    public class StreamingAssetFile : FileBase, ITextStreamingAssetFile, IBinaryStreamingAssetFile {
#if UNITY_EDITOR
        [SerializeField] UnityObject _object;
#endif

        string _streamingAssetsPath;
        public string StreamingAssetPath => _streamingAssetsPath ??= Path.GetStreamingAssetPath();

#if UNITY_EDITOR
        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string absolutePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            StreamingAssetHelper.OnBeforeSerialize(
                _object,
                ref path,
                ref relativePath,
                ref absolutePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath: StringExtensions.GetStreamingAssetPathFromAssetPath);
#endif

#if REFERENCE_ANY_PATH_FORCE_UNITASK
        public UniTask<string> ReadAllTextAsync() => ReadAllTextAsync(default);

        public UniTask<string> ReadAllTextAsync(CancellationToken cancellationToken) =>
            StreamingAssetHelper.ReadAllTextAsync(StreamingAssetPath, cancellationToken);

        public UniTask<byte[]> ReadAllBytesAsync() => ReadAllBytesAsync(default);

        public UniTask<byte[]> ReadAllBytesAsync(CancellationToken cancellationToken) =>
            StreamingAssetHelper.ReadAllBytesAsync(StreamingAssetPath, cancellationToken);
#elif UNITY_2023_1_OR_NEWER
        public Awaitable<string> ReadAllTextAsync() => ReadAllTextAsync(default);

        public Awaitable<string> ReadAllTextAsync(CancellationToken cancellationToken) =>
            StreamingAssetHelper.ReadAllTextAsync(StreamingAssetPath, cancellationToken);

        public Awaitable<byte[]> ReadAllBytesAsync() => ReadAllBytesAsync(default);

        public Awaitable<byte[]> ReadAllBytesAsync(CancellationToken cancellationToken) =>
            StreamingAssetHelper.ReadAllBytesAsync(StreamingAssetPath, cancellationToken);
#else
        public Task<string> ReadAllTextAsync() => ReadAllTextAsync(default);

        public Task<string> ReadAllTextAsync(CancellationToken cancellationToken) =>
            StreamingAssetHelper.ReadAllTextAsync(StreamingAssetPath, cancellationToken);

        public Task<byte[]> ReadAllBytesAsync() => ReadAllBytesAsync(default);

        public Task<byte[]> ReadAllBytesAsync(CancellationToken cancellationToken) =>
            StreamingAssetHelper.ReadAllBytesAsync(StreamingAssetPath, cancellationToken);
#endif
    }

    [Serializable]
    public class StreamingAssetFolder : FolderBase, IStreamingAssetFolder {
#if UNITY_EDITOR
        [SerializeField] UnityObject _object;
#endif

        string _streamingAssetsPath;
        public string StreamingAssetPath => _streamingAssetsPath ??= Path.GetStreamingAssetPath();

#if UNITY_EDITOR
        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string absolutePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            StreamingAssetHelper.OnBeforeSerialize(
                _object,
                ref path,
                ref relativePath,
                ref absolutePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath: StringExtensions.GetStreamingAssetPathFromAssetPath);
#endif
    }
}