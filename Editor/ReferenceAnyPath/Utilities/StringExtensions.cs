using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace ReferenceAnyPath {
    public static class StringExtensions {
        class PackagesLockInfo {
            [JsonProperty("dependencies")]
            public Dictionary<string, object> Dependencies { get; set; }
        }

        public static string GetAssetPathFromAbsolutePath(this string absolutePath) {
            if (string.IsNullOrEmpty(absolutePath))
                return null;

            var dataPath = Application.dataPath;
            if (absolutePath.StartsWith(dataPath))
                return "Assets" + absolutePath.Remove(0, dataPath.Length);

            var allPackages = PackageInfo.GetAllRegisteredPackages();
            foreach (var packageInfo in allPackages) {
                // For some reason, resolvedPath can have Windows separators
                var resolvedPath = packageInfo.resolvedPath.ApplyUnitySeparators();
                if (!absolutePath.StartsWith(resolvedPath))
                    continue;

                var remainder = absolutePath.Remove(0, resolvedPath.Length);
                return packageInfo.assetPath + remainder;
            }

            return null;
        }

        public static string GetExtension(this string path) {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            var extension = Path.GetExtension(path);
            extension = extension.TrimStart('.');
            return extension;
        }
    }
}