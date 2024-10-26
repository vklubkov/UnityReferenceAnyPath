using System.IO;
using UnityEditor;

namespace ReferenceAnyPath {
    public abstract class FolderValidator : WithoutExtensionValidator {
        public override bool IsValidAsset(string[] extensions, string assetPath) =>
            extensions.Length == 0 && AssetDatabase.IsValidFolder(assetPath);

        public override bool IsValidPath(string[] extensions, string absolutePath) =>
            extensions.Length == 0 && Directory.Exists(absolutePath);
    }
}