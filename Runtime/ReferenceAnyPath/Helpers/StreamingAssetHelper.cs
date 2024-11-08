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

#if UNITY_ANDROID || UNITY_WEBGL
using UnityEngine.Networking;
#else
using System.IO;
#endif

namespace ReferenceAnyPath {
    public static class StreamingAssetHelper {
#if UNITY_EDITOR
        public static void OnBeforeSerialize(
            UnityObject @object,
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error,
            Func<string, string> getRuntimePath) {
            RuntimeAssetHelper.OnBeforeSerialize(
                @object, 
                ref path,
                ref relativePath,
                ref assetPath,
                ref runtimePath, 
                ref error,
                getRuntimePath);
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
            RuntimeAssetHelper.OnBeforeSerialize(
                @object,
                ref path,
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                GetRuntimePath);

            string GetRuntimePath(string unpackedAssetPath) {
                if (width <= 0 || height <= 0 || bits <= 0 || bits % 8 != 0)
                    return null;

                return getRuntimePath.Invoke(unpackedAssetPath);
            }

            var unpackedRelativePath = relativePath.UnpackPathSimple();
            if (unpackedRelativePath != null && @object != null &&
                (width <= 0 || height <= 0 || bits <= 0 || bits % 8 != 0)) {
                error = true;
            }
        }
#endif

#if REFERENCE_ANY_PATH_FORCE_UNITASK
        public static async UniTask<string> ReadAllTextAsync(
            string unpackedStreamingAssetPath, CancellationToken cancellationToken) {
#if UNITY_ANDROID || UNITY_WEBGL
            using var request = UnityWebRequest.Get(unpackedStreamingAssetPath);
            var asyncOperation = request.SendWebRequest();
            await asyncOperation.ToUniTask(cancellationToken: cancellationToken);
            return asyncOperation.webRequest.result == UnityWebRequest.Result.Success
                ? asyncOperation.webRequest.downloadHandler.text
                : null;
#else
            await UniTask.SwitchToThreadPool();

            try {
                return await File.ReadAllTextAsync(unpackedStreamingAssetPath, cancellationToken);
            }
            finally {
                await UniTask.SwitchToMainThread();
            }
#endif
        }

        public static async UniTask<byte[]> ReadAllBytesAsync(
            string unpackedStreamingAssetPath, CancellationToken cancellationToken) {
#if UNITY_ANDROID || UNITY_WEBGL
            using var request = UnityWebRequest.Get(unpackedStreamingAssetPath);
            var asyncOperation = request.SendWebRequest();
            await asyncOperation.ToUniTask(cancellationToken: cancellationToken);
            return asyncOperation.webRequest.result == UnityWebRequest.Result.Success
                ? asyncOperation.webRequest.downloadHandler.data
                : null;
#else
            await UniTask.SwitchToThreadPool();

            try {
                return await File.ReadAllBytesAsync(unpackedStreamingAssetPath, cancellationToken);
            }
            finally {
                await UniTask.SwitchToMainThread();
            }
#endif
        }
#elif UNITY_2023_1_OR_NEWER
        public static async Awaitable<string> ReadAllTextAsync(
            string unpackedStreamingAssetPath, CancellationToken cancellationToken) {
#if UNITY_ANDROID || UNITY_WEBGL
            using var request = UnityWebRequest.Get(unpackedStreamingAssetPath);
            var asyncOperation = request.SendWebRequest();
            await Awaitable.FromAsyncOperation(asyncOperation, cancellationToken);
            return asyncOperation.webRequest.result == UnityWebRequest.Result.Success
                ? asyncOperation.webRequest.downloadHandler.text
                : null;
#else
            await Awaitable.BackgroundThreadAsync();

            try {
                return await File.ReadAllTextAsync(unpackedStreamingAssetPath, cancellationToken);
            }
            finally {
                await Awaitable.MainThreadAsync();
            }
#endif
        }

        public static async Awaitable<byte[]> ReadAllBytesAsync(
            string unpackedStreamingAssetPath, CancellationToken cancellationToken) {
#if UNITY_ANDROID || UNITY_WEBGL
            using var request = UnityWebRequest.Get(unpackedStreamingAssetPath);
            var asyncOperation = request.SendWebRequest();
            await Awaitable.FromAsyncOperation(asyncOperation, cancellationToken);
            return asyncOperation.webRequest.result == UnityWebRequest.Result.Success
                ? asyncOperation.webRequest.downloadHandler.data
                : null;
#else
            await Awaitable.BackgroundThreadAsync();

            try {
                return await File.ReadAllBytesAsync(unpackedStreamingAssetPath, cancellationToken);
            }
            finally {
                await Awaitable.MainThreadAsync();
            }
#endif
        }
#else
        public static async Task<string> ReadAllTextAsync(
            string unpackedStreamingAssetPath, CancellationToken cancellationToken) {
#if UNITY_ANDROID || UNITY_WEBGL
            using var request = UnityWebRequest.Get(unpackedStreamingAssetPath);
            var asyncOperation = request.SendWebRequest();
            while (!asyncOperation.isDone) {
                await Task.Yield();
                cancellationToken.ThrowIfCancellationRequested();
            }

            return asyncOperation.webRequest.result == UnityWebRequest.Result.Success
                ? asyncOperation.webRequest.downloadHandler.text
                : null;
#else
            return await File.ReadAllTextAsync(unpackedStreamingAssetPath, cancellationToken);
#endif
        }

        public static async Task<byte[]> ReadAllBytesAsync(
            string unpackedStreamingAssetPath, CancellationToken cancellationToken) {
#if UNITY_ANDROID || UNITY_WEBGL
            using var request = UnityWebRequest.Get(unpackedStreamingAssetPath);
            var asyncOperation = request.SendWebRequest();
            while (!asyncOperation.isDone) {
                await Task.Yield();
                cancellationToken.ThrowIfCancellationRequested();
            }

            return asyncOperation.webRequest.result == UnityWebRequest.Result.Success
                ? asyncOperation.webRequest.downloadHandler.data
                : null;
#else
            return await File.ReadAllBytesAsync(unpackedStreamingAssetPath, cancellationToken);
#endif
        }
#endif
    }
}