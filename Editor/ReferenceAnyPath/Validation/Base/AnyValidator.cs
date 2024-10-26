using System.IO;
using UnityEditor;

namespace ReferenceAnyPath {
    public abstract class AnyValidator : WithExtensionValidator {
        public override bool IsValidAsset(string[] extensions, string assetPath) {
            if (!assetPath.AssetPathExists())
                return false;

            if (AssetDatabase.IsValidFolder(assetPath))
                return extensions.Length == 0;

            return true;
        }

        public override bool IsValidPath(string[] extensions, string absolutePath) {
            if (Directory.Exists(absolutePath))
                return extensions.Length == 0;

            return File.Exists(absolutePath);
        }
    }
}