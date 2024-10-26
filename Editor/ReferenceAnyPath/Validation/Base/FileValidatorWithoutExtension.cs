using System.IO;
using UnityEditor;

namespace ReferenceAnyPath {
    public abstract class FileValidatorWithoutExtension : WithoutExtensionValidator {
        public override bool IsValidAsset(string[] extensions, string assetPath) =>
            extensions.Length == 0 && assetPath.AssetPathExists() && !AssetDatabase.IsValidFolder(assetPath);

        public override bool IsValidPath(string[] extensions, string absolutePath) =>
            extensions.Length == 0 && File.Exists(absolutePath);
    }
}