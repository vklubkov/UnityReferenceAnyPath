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
    public class RawHeightmapFile : Base, IRawHeightmapFile {
#if UNITY_EDITOR
#pragma warning disable 0414 // Used in PropertyDrawer
        [SerializeField] string _extensions = "raw, r16, r32, bytes";
#pragma warning restore 0414
#endif

        [SerializeField] int _width;
        [SerializeField] int _height;
        [SerializeField] int _bits;
        [SerializeField] ByteOrder _byteOrder;
        [SerializeField] Flip _flip;

        public int Width => _width;
        public int Height => _height;
        public int Bits => _bits;
        public ByteOrder ByteOrder => _byteOrder;
        public Flip Flip => _flip;

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
            PathHelper.OnBeforeSerialize(
                _width,
                _height,
                _bits,
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error);
#endif
    }

    [Serializable]
    public class RawHeightmapAsset : Base, IRawHeightmapAsset {
#if UNITY_EDITOR
#pragma warning disable 0414 // Used in PropertyDrawer
        [SerializeField] string _extensions = "raw, r16, r32, bytes";
#pragma warning restore 0414

        [SerializeField] UnityObject _object;
#endif

        [SerializeField] int _width;
        [SerializeField] int _height;
        [SerializeField] int _bits;
        [SerializeField] ByteOrder _byteOrder;
        [SerializeField] Flip _flip;

        public int Width => _width;
        public int Height => _height;
        public int Bits => _bits;
        public ByteOrder ByteOrder => _byteOrder;
        public Flip Flip => _flip;

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
            AssetHelper.OnBeforeSerialize(
                _object,
                _width,
                _height,
                _bits,
                ref path,
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath: StringExtensions.GetRuntimePathFromAssetPath);
#endif
    }

    [Serializable]
    public class RawHeightmapRuntimeAsset : Base, IRawHeightmapRuntimeAsset {
#if UNITY_EDITOR
#pragma warning disable 0414 // Used in PropertyDrawer
        [SerializeField] string _extensions = "raw, r16, r32, bytes";
#pragma warning restore 0414

        [SerializeField] UnityObject _object;
#endif

        [SerializeField] int _width;
        [SerializeField] int _height;
        [SerializeField] int _bits;
        [SerializeField] ByteOrder _byteOrder;
        [SerializeField] Flip _flip;

        public int Width => _width;
        public int Height => _height;
        public int Bits => _bits;
        public ByteOrder ByteOrder => _byteOrder;
        public Flip Flip => _flip;

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
            RuntimeAssetHelper.OnBeforeSerialize(
                _object,
                _width,
                _height,
                _bits,
                ref path,
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath: StringExtensions.GetRuntimePathFromAssetPath);
#endif
    }

    [Serializable]
    public class RawHeightmapResource : Base, IRawHeightmapResource {
#if UNITY_EDITOR
#pragma warning disable 0414 // Used in PropertyDrawer
        [SerializeField] string _extensions = "bytes";
#pragma warning restore 0414

        [SerializeField] TextAsset _object;
#endif

        [SerializeField] int _width;
        [SerializeField] int _height;
        [SerializeField] int _bits;
        [SerializeField] ByteOrder _byteOrder;
        [SerializeField] Flip _flip;

        public int Width => _width;
        public int Height => _height;
        public int Bits => _bits;
        public ByteOrder ByteOrder => _byteOrder;
        public Flip Flip => _flip;

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
                _width,
                _height,
                _bits,
                ref path,
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath: StringExtensions.GetBinaryResourcePathFromAssetPath);
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
    public class RawHeightmapStreamingAsset : Base, IRawHeightmapStreamingAsset {
#if UNITY_EDITOR
#pragma warning disable 0414 // Used in PropertyDrawer
        [SerializeField] string _extensions = "raw, r16, r32, bytes";
#pragma warning restore 0414

        [SerializeField] UnityObject _object;
#endif

        [SerializeField] int _width;
        [SerializeField] int _height;
        [SerializeField] int _bits;
        [SerializeField] ByteOrder _byteOrder;
        [SerializeField] Flip _flip;

        string _streamingAssetsPath;
        public string StreamingAssetPath => _streamingAssetsPath ??= Path.GetStreamingAssetPath();

        public int Width => _width;
        public int Height => _height;
        public int Bits => _bits;
        public ByteOrder ByteOrder => _byteOrder;
        public Flip Flip => _flip;

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
            StreamingAssetHelper.OnBeforeSerialize(
                _object,
                _width,
                _height,
                _bits,
                ref path,
                ref relativePath,
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
}