using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ReferenceAnyPath {
    [Serializable]
    public abstract class AnyBase : Base {
        public override string Path => PathUnsafe.UnpackPathComplex();

#if UNITY_EDITOR
        [SerializeField, Preserve] string _extensions = string.Empty;

        public override string RelativePath {
            get {
                var unpackedRelativePath = RelativePathUnsafe.UnpackPathComplex();
                var unpackedAbsolutePath = unpackedRelativePath.GetAbsolutePathFromRelativePath();
                return unpackedAbsolutePath.DoesPathExist() ? unpackedRelativePath : null;
            }
        }

        public override string AbsolutePath {
            get {
                var unpackedRelativePath = RelativePathUnsafe.UnpackPathComplex();
                var unpackedAbsolutePath = unpackedRelativePath.GetAbsolutePathFromRelativePath();
                return unpackedAbsolutePath.DoesPathExist() ? unpackedAbsolutePath : null;
            }
        }

        public override string AssetPath {
            get {
                var unpackedAssetPath = AssetPathUnsafe.UnpackPathSimple();
                return unpackedAssetPath.DoesAssetExist() ? unpackedAssetPath : null;
            }
        }

        public override string RuntimePath =>
            AssetPathUnsafe.UnpackPathSimple().DoesAssetExist()
                ? RuntimePathUnsafe.UnpackPathComplex()
                : null;
#endif
    }
}