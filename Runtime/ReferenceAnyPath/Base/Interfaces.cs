using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityObject = UnityEngine.Object;

#if REFERENCE_ANY_PATH_FORCE_UNITASK
using Cysharp.Threading.Tasks;
#endif

#if !UNITY_2023_1_OR_NEWER
using System.Threading.Tasks;
#endif

namespace ReferenceAnyPath {
#if UNITY_EDITOR
    public interface IEditorPaths {
        public string RelativePath { get; }
        public string AbsolutePath { get; }
        public string AssetPath { get; }
        public string RuntimePath { get; }
    }

    public interface IEditorPathsUnsafe : IEditorPaths {
        public string RelativePathUnsafe { get; }
        public string AbsolutePathUnsafe { get; }
        public string AssetPathUnsafe { get; }
        public string RuntimePathUnsafe { get; }
    }
#endif

#if UNITY_EDITOR
    public interface IPath : IEditorPaths {
#else
    public interface IPath {
#endif
        public string Path { get; }
    }

#if UNITY_EDITOR
    public interface IPathUnsafe : IPath, IEditorPathsUnsafe {
#else
    public interface IPathUnsafe : IPath {
#endif
        public string PathUnsafe { get; }
    }

    public interface IAnyPath : IPathUnsafe { }
    public interface IAnyFile : IAnyPath { }
    public interface IAnyFolder : IAnyPath { }

    public interface IAsset : IAnyPath { }
    public interface IAssetFile : IAsset, IAnyFile { }
    public interface IAssetFolder : IAsset, IAnyFolder { }

    public interface IRuntimeAsset : IAsset { }
    public interface IRuntimeFile : IRuntimeAsset, IAssetFile { }
    public interface IRuntimeFolder : IRuntimeAsset, IAssetFolder { }

    public interface IResource : IRuntimeAsset { }

    public interface IResourceFile : IResource, IRuntimeFile { }

    public interface ITextResourceFile : IResourceFile {
        public string Load();

#if REFERENCE_ANY_PATH_FORCE_UNITASK
        public UniTask<string> LoadAsync();
        public UniTask<string> LoadAsync(CancellationToken cancellationToken);
#elif UNITY_2023_1_OR_NEWER
        public Awaitable<string> LoadAsync();
        public Awaitable<string> LoadAsync(CancellationToken cancellationToken);
#else
        public Task<string> LoadAsync();
        public Task<string> LoadAsync(CancellationToken cancellationToken);
#endif
    }

    public interface IBinaryResourceFile : IResourceFile {
        public byte[] Load();

#if REFERENCE_ANY_PATH_FORCE_UNITASK
        public UniTask<byte[]> LoadAsync();
        public UniTask<byte[]> LoadAsync(CancellationToken cancellationToken);
#elif UNITY_2023_1_OR_NEWER
        public Awaitable<byte[]> LoadAsync();
        public Awaitable<byte[]> LoadAsync(CancellationToken cancellationToken);
#else
        public Task<byte[]> LoadAsync();
        public Task<byte[]> LoadAsync(CancellationToken cancellationToken);
#endif
    }

    public interface IResourceFile<TObject> : IResourceFile {
        public TObject Load();
        public T Load<T>() where T : TObject;
        public TObject Load(Type systemTypeInstance);
        public ResourceRequest LoadAsync();
        public ResourceRequest LoadAsync<T>() where T : TObject;
        public ResourceRequest LoadAsync(Type systemTypeInstance);
    }

    public interface IResourceFolder : IResource, IRuntimeFolder {
        public UnityObject[] LoadAll();
        public T[] LoadAll<T>() where T : UnityObject;
        public UnityObject[] LoadAll(Type systemTypeInstance);
    }

    public interface IStreamingAsset : IRuntimeAsset {
        public string StreamingAssetPath { get; }
    }

    public interface IStreamingAssetFile : IStreamingAsset, IRuntimeFile { }

    public interface ITextStreamingAssetFile : IStreamingAssetFile {
#if REFERENCE_ANY_PATH_FORCE_UNITASK
        public UniTask<string> ReadAllTextAsync();
        public UniTask<string> ReadAllTextAsync(CancellationToken cancellationToken);
#elif UNITY_2023_1_OR_NEWER
        public Awaitable<string> ReadAllTextAsync();
        public Awaitable<string> ReadAllTextAsync(CancellationToken cancellationToken);
#else
        public Task<string> ReadAllTextAsync();
        public Task<string> ReadAllTextAsync(CancellationToken cancellationToken);
#endif
    }

    public interface IBinaryStreamingAssetFile : IStreamingAssetFile {
#if REFERENCE_ANY_PATH_FORCE_UNITASK
        public UniTask<byte[]> ReadAllBytesAsync();
        public UniTask<byte[]> ReadAllBytesAsync(CancellationToken cancellationToken);
#elif UNITY_2023_1_OR_NEWER
        public Awaitable<byte[]> ReadAllBytesAsync();
        public Awaitable<byte[]> ReadAllBytesAsync(CancellationToken cancellationToken);
#else
        public Task<byte[]> ReadAllBytesAsync();
        public Task<byte[]> ReadAllBytesAsync(CancellationToken cancellationToken);
#endif
    }

    public interface IStreamingAssetFolder : IStreamingAsset, IRuntimeFolder { }

    public interface IScene : IRuntimeFile {
        public string Name { get; }
        public void LoadScene();
        public void LoadScene(LoadSceneMode mode);
        public void LoadScene(LoadSceneParameters parameters);
        public AsyncOperation LoadSceneAsync();
        public AsyncOperation LoadSceneAsync(LoadSceneMode mode);
        public AsyncOperation LoadSceneAsync(LoadSceneParameters parameters);
    }

    public interface IRawHeightmap : IAnyFile {
        public int Width { get;}
        public int Height { get;}
        public int Bits { get;}
        public ByteOrder ByteOrder { get; }
        public Flip Flip { get; }
    }

    public interface IRawHeightmapFile : IRawHeightmap { }
    public interface IRawHeightmapAsset : IRawHeightmap, IAssetFile { }
    public interface IRawHeightmapRuntimeAsset : IRawHeightmap, IRuntimeFile { }
    public interface IRawHeightmapResource : IRawHeightmap, IBinaryResourceFile { }
    public interface IRawHeightmapStreamingAsset : IRawHeightmap, IBinaryStreamingAssetFile { }
}