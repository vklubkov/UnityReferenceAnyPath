using System.IO;
using UnityEditor;

namespace ReferenceAnyPath {
    public abstract class FileValidatorWithExtension : WithExtensionValidator {
        public override bool IsValidAsset(string[] extensions, string assetPath) =>
            assetPath.AssetPathExists() && !AssetDatabase.IsValidFolder(assetPath);

        public override bool IsValidPath(string[] extensions, string absolutePath) => File.Exists(absolutePath);
    }
}