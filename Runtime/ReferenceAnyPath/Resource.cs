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
    public class Resource : AnyBase, IResource {
#if UNITY_EDITOR
        [SerializeField] UnityObject _object;

        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            ResourceHelper.OnBeforeSerialize(
                _object,
                ref path,
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath: StringExtensions.GetResourcePathFromAssetPath);
#endif
    }

    [Serializable]
    public class TextResource : FileBase, ITextResourceFile {
#if UNITY_EDITOR
        [SerializeField] TextAsset _object;

        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            ResourceHelper.OnBeforeSerialize(
                _object,
                ref path,
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath: StringExtensions.GetResourcePathFromAssetPath);
#endif

        public string Load() => ResourceHelper.LoadTextAsset(Path);

#if REFERENCE_ANY_PATH_FORCE_UNITASK
        public UniTask<string> LoadAsync() => LoadAsync(default);

        public UniTask<string> LoadAsync(CancellationToken cancellationToken) =>
            ResourceHelper.LoadTextAssetAsync(Path, cancellationToken);
#elif UNITY_2023_1_OR_NEWER
        public Awaitable<string> LoadAsync() => LoadAsync(default);

        public Awaitable<string> LoadAsync(CancellationToken cancellationToken) =>
            ResourceHelper.LoadTextAssetAsync(Path, cancellationToken);
#else
        public Task<string> LoadAsync() => LoadAsync(default);

        public Task<string> LoadAsync(CancellationToken cancellationToken) =>
            ResourceHelper.LoadTextAssetAsync(Path, cancellationToken);
#endif
    }

    [Serializable]
    public class BinaryResource : Base, IBinaryResourceFile {
#if UNITY_EDITOR
#pragma warning disable 0414 // Used in PropertyDrawer
        [SerializeField] string _extensions = "bytes";
#pragma warning restore 0414

        [SerializeField] TextAsset _object;
#endif

        public override string Path => PathUnsafe.UnpackPathSimple();

#if UNITY_EDITOR
        public override string RelativePath {
            get {
                var unpackedRelativePath = RelativePathUnsafe.UnpackPathSimple();
                var absolutePath = unpackedRelativePath.GetAbsolutePathFromRelativePath();
                return absolutePath.DoesFileExist() ? unpackedRelativePath : null;
            }
        }

        public override string AbsolutePath {
            get {
                var unpackedRelativePath = RelativePathUnsafe.UnpackPathSimple();
                var absolutePath = unpackedRelativePath.GetAbsolutePathFromRelativePath();
                return absolutePath.DoesFileExist() ? absolutePath : null;
            }
        }

        public override string AssetPath {
            get {
                var unpackedAssetPath = AssetPathUnsafe.UnpackPathSimple();
                return unpackedAssetPath.DoesAssetFileExist() ? unpackedAssetPath : null;
            }
        }

        public override string RuntimePath =>
            AssetPathUnsafe.UnpackPathSimple().DoesAssetFileExist()
                ? RuntimePathUnsafe.UnpackPathSimple()
                : null;

        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            ResourceHelper.OnBeforeSerialize(
                _object,
                ref path,
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath: StringExtensions.GetResourcePathFromAssetPath);
#endif

        public byte[] Load() => ResourceHelper.LoadBinaryAsset(Path);

#if REFERENCE_ANY_PATH_FORCE_UNITASK
        public UniTask<byte[]> LoadAsync() => LoadAsync(default);

        public UniTask<byte[]> LoadAsync(CancellationToken cancellationToken) =>
            ResourceHelper.LoadBinaryAssetAsync(Path, cancellationToken);
#elif UNITY_2023_1_OR_NEWER
        public Awaitable<byte[]> LoadAsync() => LoadAsync(default);

        public Awaitable<byte[]> LoadAsync(CancellationToken cancellationToken) =>
            ResourceHelper.LoadBinaryAssetAsync(Path, cancellationToken);
#else
        public Task<byte[]> LoadAsync() => LoadAsync(default);

        public Task<byte[]> LoadAsync(CancellationToken cancellationToken) =>
            ResourceHelper.LoadBinaryAssetAsync(Path, cancellationToken);
#endif
    }

    [Serializable]
    public class Resource<TObject> : FileBase, IResourceFile<TObject> where TObject : UnityObject {
#if UNITY_EDITOR
        [SerializeField] TObject _object;

        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            ResourceHelper.OnBeforeSerialize(
                _object,
                ref path,
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath: StringExtensions.GetResourcePathFromAssetPath);
#endif

        public TObject Load() => Resources.Load(Path, typeof(TObject)) as TObject;
        public T Load<T>() where T : TObject => Resources.Load<T>(Path);
        public TObject Load(Type systemTypeInstance) => Resources.Load(Path, systemTypeInstance) as TObject;
        public ResourceRequest LoadAsync() => Resources.LoadAsync(Path, typeof(TObject));
        public ResourceRequest LoadAsync<T>() where T : TObject => Resources.LoadAsync<T>(Path);
        public ResourceRequest LoadAsync(Type systemTypeInstance) => Resources.LoadAsync(Path, systemTypeInstance);
    }

    [Serializable]
    public class ResourceFile : FileBase, IResourceFile<UnityObject> {
#if UNITY_EDITOR
        [SerializeField] UnityObject _object;

        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            ResourceHelper.OnBeforeSerialize(
                _object,
                ref path,
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath: StringExtensions.GetResourcePathFromAssetPath);
#endif

        public UnityObject Load() => Resources.Load(Path, typeof(UnityObject));
        public T Load<T>() where T : UnityObject => Resources.Load<T>(Path);
        public UnityObject Load(Type systemTypeInstance) => Resources.Load(Path, systemTypeInstance);
        public ResourceRequest LoadAsync() => Resources.LoadAsync(Path, typeof(UnityObject));
        public ResourceRequest LoadAsync<T>() where T : UnityObject => Resources.LoadAsync<T>(Path);
        public ResourceRequest LoadAsync(Type systemTypeInstance) => Resources.LoadAsync(Path, systemTypeInstance);
    }

    [Serializable]
    public class ResourceFolder : FolderBase, IResourceFolder {
#if UNITY_EDITOR
        [SerializeField] UnityObject _object;

        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            ResourceHelper.OnBeforeSerialize(
                _object,
                ref path,
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath: StringExtensions.GetResourcePathFromAssetPath);
#endif

        // Check the path for null to make it behave the same way as resource file loading behaves.

        public UnityObject[] LoadAll() {
            var path = Path;
            return path == null ? Array.Empty<UnityObject>() : Resources.LoadAll(path, typeof(UnityObject));
        }

        public T[] LoadAll<T>() where T : UnityObject {
            var path = Path;
            return path == null ? Array.Empty<T>() : Resources.LoadAll<T>(path);
        }

        public UnityObject[] LoadAll(Type systemTypeInstance) {
            var path = Path;
            return path == null ? Array.Empty<UnityObject>() : Resources.LoadAll(path, systemTypeInstance);
        }
    }
}