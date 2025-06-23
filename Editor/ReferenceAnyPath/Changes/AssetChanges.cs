using UnityEditor;
using UnityObject = UnityEngine.Object;

namespace ReferenceAnyPath {
    public abstract class AssetChanges<TValidator> : Changes<TValidator> where TValidator : Validator {
        public override void Apply() {
            var inputProperty = Property.MainProperty;
            var newObject = inputProperty.objectReferenceValue;
            if (newObject == null) {
                SetProperties(null, null, null, null);
                return;
            }

            var unpackedAssetPath = AssetDatabase.GetAssetPath(newObject);
            if (!Validator.IsValidAsset(Validator.Extensions, unpackedAssetPath))
                return;

            var unpackedRelativePath = unpackedAssetPath.GetRelativePathFromAssetPath();
            var unpackedAbsolutePath = unpackedRelativePath.GetAbsolutePathFromRelativePath();
            if (!IsValidPath(unpackedAbsolutePath))
                return;

            var unpackedRuntimePath = unpackedAssetPath.GetRuntimePathFromAssetPath();
            SetProperties(newObject, unpackedRelativePath, unpackedAssetPath, unpackedRuntimePath);
        }

        void SetProperties(UnityObject @object, string relativePath, string assetPath, string runtimePath) {
            Property.SetObject(PropertyName._object, @object);

            var path = relativePath.PackPathComplex();
            Property.SetString(PropertyName._path, path);
            Property.SetString(PropertyName._relativePath, path);
            Property.SetString(PropertyName._assetPath, assetPath.PackPathSimple());
            Property.SetString(PropertyName._runtimePath, runtimePath.PackPathComplex());
        }

        protected abstract bool IsValidPath(string absolutePath);
    }
}