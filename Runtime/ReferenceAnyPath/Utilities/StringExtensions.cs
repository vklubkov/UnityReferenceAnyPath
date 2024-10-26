#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
#endif

using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace ReferenceAnyPath {
    public static class StringExtensions {
#if UNITY_EDITOR
        const string _packages = "Packages";
        const string _assets = "Assets";
        const string _streamingAssets = _assets + "/StreamingAssets";
        const string _resources = "/Resources";
        const string _sceneExtension = ".unity";
        const string _binaryResourceExtension = ".bytes";


        // Don't use every frame

        public static bool DoesPathExist(this string absolutePath) =>
            absolutePath != null && (File.Exists(absolutePath) || Directory.Exists(absolutePath));

        public static bool DoesAssetExist(this string assetPath) =>
            assetPath != null && assetPath.AssetPathExists();

        public static bool DoesFileExist(this string absolutePath) =>
            absolutePath != null && File.Exists(absolutePath);

        public static bool DoesAssetFileExist(this string assetPath) =>
            assetPath != null && assetPath.AssetPathExists() && !AssetDatabase.IsValidFolder(assetPath);

        public static bool DoesFolderExist(this string absolutePath) =>
            absolutePath != null && Directory.Exists(absolutePath);

        public static bool DoesAssetFolderExist(this string assetPath) =>
            assetPath != null && AssetDatabase.IsValidFolder(assetPath);

        public static bool AssetPathExists(this string assetPath) {
#if UNITY_2023_1_OR_NEWER
            return AssetDatabase.AssetPathExists(assetPath);
#else
            var guid = AssetDatabase.AssetPathToGUID(assetPath, AssetPathToGUIDOptions.OnlyExistingAssets);
            return !string.IsNullOrEmpty(guid);
#endif
        }


        // For use every frame

        public static string GetRuntimePathFromAssetPath(this string assetPath) {
            var streamingAssetPath = assetPath.GetStreamingAssetPathFromAssetPath();
            if (streamingAssetPath != null)
                return streamingAssetPath;

            var resourcePath = assetPath.GetResourcePathFromAssetPath();
            if (resourcePath != null)
                return resourcePath;

            return assetPath.GetScenePathFromAssetPath();
        }

        public static string GetStreamingAssetPathFromAssetPath(this string assetPath) {
            if (string.IsNullOrEmpty(assetPath))
                return null;

            if (!assetPath.StartsWith(_streamingAssets))
                return null; // Not a streaming asset

            if (assetPath.Length == _streamingAssets.Length)
                return string.Empty; // StreamingAssets folder itself

            var potentialStreamingAsset = assetPath.Remove(0, _streamingAssets.Length);
            if (potentialStreamingAsset.StartsWith('/'))
                return potentialStreamingAsset.Remove(0, 1); // Path within StreamingAssets folder

            return null; // Not a streaming asset
        }

        public static string GetBinaryResourcePathFromAssetPath(this string assetPath) {
            var resourcePath = assetPath.GetResourcePathFromAssetPath();
            if (resourcePath == null)
                return null;

            if (!assetPath.EndsWith(_binaryResourceExtension))
                return null;

            return resourcePath;
        }

        public static string GetResourcePathFromAssetPath(this string assetPath) {
            if (string.IsNullOrEmpty(assetPath))
                return null;

            if (assetPath.StartsWith(_streamingAssets))
                return null; // Streaming asset

            var index = assetPath.IndexOf(_resources, StringComparison.InvariantCulture);
            if (index == -1)
                return null; // Resources folder not found

            if (assetPath.Length == index + _resources.Length)
                return string.Empty; // Resources folder itself

            var potentialResource = assetPath.Remove(0, index + _resources.Length);
            if (!potentialResource.StartsWith('/'))
                return null; // Not a resource

            var resourcesPath = potentialResource.Remove(0, 1); // Path within Resources folder
            return AssetDatabase.IsValidFolder(assetPath) ? resourcesPath : RemoveExtension(resourcesPath);
        }

        static string RemoveExtension(string path) => ApplyUnitySeparators(Path.ChangeExtension(path, null));

        public static string GetScenePathFromAssetPath(this string assetPath) {
            if (string.IsNullOrEmpty(assetPath))
                return null;

            if (!assetPath.EndsWith(_sceneExtension))
                return null; // Not a scene

            if (assetPath.StartsWith(_assets)) {
                assetPath = assetPath.Remove(0, _assets.Length);
                if (!assetPath.StartsWith('/'))
                    return null; // Not in the Assets folder

                assetPath = assetPath.TrimStart('/');
                return GetSceneAssetPath(assetPath);
            }

            if (assetPath.StartsWith(_packages)) {
                var tempPath = assetPath.Remove(0, _packages.Length);
                if (!tempPath.StartsWith('/'))
                    return null; // Not a Package

                return GetSceneAssetPath(assetPath);
            }

            return null; // Not a Scene
        }

        static string GetSceneAssetPath(string path) {
            var parentPath = path.GetParentPath();
            var assetNameWithoutExtension = path.GetFileNameWithoutExtension();
            var sceneName = string.IsNullOrEmpty(parentPath)
                ? assetNameWithoutExtension
                : parentPath + '/' + assetNameWithoutExtension;

            return string.IsNullOrEmpty(sceneName) ? null : sceneName;
        }

        public static string GetRelativePathFromAssetPath(this string assetPath) {
            if (string.IsNullOrEmpty(assetPath))
                return null;

            if (assetPath.StartsWith(_assets)) {
                if (assetPath.Length == _assets.Length) {
                    return "."; // Assets folder itself
                }

                assetPath = assetPath.Remove(0, _assets.Length);
                if (!assetPath.StartsWith('/')) {
                    return null; // Not in the Assets folder
                }

                var absolutePath = Application.dataPath + assetPath;
                return absolutePath.GetRelativePath(); // An asset within the Assets folder
            }

            if (assetPath.StartsWith(_packages)) {
                var processedAssetPath = assetPath.Remove(0, _packages.Length);
                if (!processedAssetPath.StartsWith('/')) {
                    return null; // Not a Package
                }

                processedAssetPath = processedAssetPath.Trim('/');
                if (string.IsNullOrEmpty(processedAssetPath)) {
                    return null; // Not a Package
                }

                var packageInfo = PackageInfo.FindForAssetPath(assetPath);
                if (packageInfo == null)
                    return null; // Not a valid Package

                var packageRelativePath = packageInfo.resolvedPath.GetRelativePath();
                if (string.IsNullOrEmpty(packageRelativePath))
                    return null; // Not a valid Package

                var firstSeparatorPosition = processedAssetPath.IndexOf('/');
                if (firstSeparatorPosition == -1)
                    return Path.IsPathRooted(packageRelativePath)
                       ? packageRelativePath.GetRelativePath()
                       : packageRelativePath; // Package folder itself

                processedAssetPath = processedAssetPath.Remove(0, firstSeparatorPosition);
                var path = packageRelativePath + processedAssetPath; // An asset within a Package
                return Path.IsPathRooted(path) ? path.GetRelativePath() : path;
            }

            return null; // Not an asset
        }

        public static string GetRelativePathFromAbsolutePath(this string absolutePath) =>
            string.IsNullOrEmpty(absolutePath) ? null : absolutePath.GetRelativePath();

        static string GetRelativePath(this string path) =>
            Path.GetRelativePath(Application.dataPath, path).ApplyUnitySeparators();

        public static string GetAbsolutePathFromRelativePath(this string relativePath) =>
            string.IsNullOrEmpty(relativePath) ? null : relativePath.GetFullPath();

        static string GetFullPath(this string path) {
            if (Path.IsPathRooted(path))
                return path;

            return Path.GetFullPath(Application.dataPath + "/" + path).ApplyUnitySeparators();
        }

        static string GetParentPath(this string path) =>
            Path.GetDirectoryName(path).ApplyUnitySeparators();

        static string GetFileNameWithoutExtension(this string path) =>
            Path.GetFileNameWithoutExtension(path);

        public static string GetFileName(this string path) => Path.GetFileName(path);

        public static string ApplyUnitySeparators(this string path) {
#if UNITY_EDITOR_WIN
            return path.Replace('\\', '/');
#else
            return path;
#endif
        }
#endif


        // For use in runtime

        public static string GetStreamingAssetPath(this string path) {
            if (path == null)
                return null;

            var streamingAssetsPath = Application.streamingAssetsPath;
            return path == string.Empty
                ? streamingAssetsPath
                : streamingAssetsPath + '/' + path;
        }

        // Pack/unpack paths that cannot be "current directory" paths
        public static string PackPathSimple(this string filePath) => filePath ?? string.Empty;
        public static string UnpackPathSimple(this string filePath) => string.IsNullOrEmpty(filePath) ? null : filePath;

        // Pack/unpack paths that can be "current directory" paths
        public static string PackPathComplex(this string folderPath) =>
            folderPath == null
                ? string.Empty
                : string.IsNullOrEmpty(folderPath)
                    ? "."
                    : folderPath;

        public static string UnpackPathComplex(this string folder) =>
            string.IsNullOrEmpty(folder)
                ? null
                : folder == "."
                    ? string.Empty
                    : folder;
    }
}