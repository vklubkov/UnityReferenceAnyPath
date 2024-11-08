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
    public static class ResourceHelper {
#if UNITY_EDITOR
        public static void OnBeforeSerialize(
            UnityObject @object,
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error,
            Func<string, string> getRuntimePath) =>
            RuntimeAssetHelper.OnBeforeSerialize(
                @object,
                ref path,
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref error,
                getRuntimePath);

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
                if (width <= 0 || height <= 0 || bits <= 0)
                    return null;

                return getRuntimePath.Invoke(unpackedAssetPath);
            }

            var unpackedRelativePath = relativePath.UnpackPathSimple();
            if (unpackedRelativePath != null && @object != null && (width <= 0 || height <= 0 || bits <= 0)) {
                error = true;
            }
        }
#endif

        public static string LoadTextAsset(string unpackedResourcePath) {
            var textAsset = Resources.Load<TextAsset>(unpackedResourcePath);
            return textAsset == null ? string.Empty : textAsset.text;
        }

#if REFERENCE_ANY_PATH_FORCE_UNITASK
        public static async UniTask<string> LoadTextAssetAsync(
            string unpackedResourcePath, CancellationToken cancellationToken) {
            var asyncOperation = Resources.LoadAsync<TextAsset>(unpackedResourcePath);
            if (asyncOperation == null)
                return null;

            await asyncOperation.ToUniTask(cancellationToken: cancellationToken);
            var textAsset = asyncOperation.asset as TextAsset;
            return textAsset == null ? null : textAsset.text;
        }
#elif UNITY_2023_1_OR_NEWER
        public static async Awaitable<string> LoadTextAssetAsync(
            string unpackedResourcePath, CancellationToken cancellationToken) {
            var asyncOperation = Resources.LoadAsync<TextAsset>(unpackedResourcePath);
            if (asyncOperation == null)
                return null;

            await Awaitable.FromAsyncOperation(asyncOperation, cancellationToken);
            var textAsset = asyncOperation.asset as TextAsset;
            return textAsset == null ? null : textAsset.text;
        }
#else
        public static async Task<string> LoadTextAssetAsync(
            string unpackedResourcePath, CancellationToken cancellationToken) {
            var asyncOperation = Resources.LoadAsync<TextAsset>(unpackedResourcePath);
            if (asyncOperation == null)
                return null;

            while (!asyncOperation.isDone) {
                await Task.Yield();
                cancellationToken.ThrowIfCancellationRequested();
            }

            var textAsset = asyncOperation.asset as TextAsset;
            return textAsset == null ? null : textAsset.text;
        }
#endif

        public static byte[] LoadBinaryAsset(string unpackedResourcePath) {
            var textAsset = Resources.Load<TextAsset>(unpackedResourcePath);
            return textAsset == null ? Array.Empty<byte>() : textAsset.bytes;
        }

#if REFERENCE_ANY_PATH_FORCE_UNITASK
        public static async UniTask<byte[]> LoadBinaryAssetAsync(
            string unpackedResourcePath, CancellationToken cancellationToken) {
            var asyncOperation = Resources.LoadAsync<TextAsset>(unpackedResourcePath);
            if (asyncOperation == null)
                return null;

            await asyncOperation.ToUniTask(cancellationToken: cancellationToken);
            var textAsset = asyncOperation.asset as TextAsset;
            return textAsset == null ? null : textAsset.bytes;
        }
#elif UNITY_2023_1_OR_NEWER
        public static async Awaitable<byte[]> LoadBinaryAssetAsync(
            string unpackedResourcePath, CancellationToken cancellationToken) {
            var asyncOperation = Resources.LoadAsync<TextAsset>(unpackedResourcePath);
            if (asyncOperation == null)
                return null;

            await Awaitable.FromAsyncOperation(asyncOperation, cancellationToken);
            var textAsset = asyncOperation.asset as TextAsset;
            return textAsset == null ? null : textAsset.bytes;
        }
#else
        public static async Task<byte[]> LoadBinaryAssetAsync(
            string unpackedResourcePath, CancellationToken cancellationToken) {
            var asyncOperation = Resources.LoadAsync<TextAsset>(unpackedResourcePath);
            if (asyncOperation == null)
                return null;

            while (!asyncOperation.isDone) {
                await Task.Yield();
                cancellationToken.ThrowIfCancellationRequested();
            }

            var textAsset = asyncOperation.asset as TextAsset;
            return textAsset == null ? null : textAsset.bytes;
        }
#endif
    }
}