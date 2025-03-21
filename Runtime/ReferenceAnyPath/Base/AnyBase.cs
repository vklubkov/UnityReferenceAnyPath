using System;
using UnityEngine;

namespace ReferenceAnyPath {
    [Serializable]
    public abstract class AnyBase : Base {
        public override string Path => PathUnsafe.UnpackPathComplex();

#if UNITY_EDITOR
        // ReSharper disable UnusedMember.Local because it is used in PropertyDrawer
        [SerializeField] string _extensions = string.Empty;
        // ReSharper restore UnusedMember.Local

        public override string RelativePath {
            get {
                var unpackedRelativePath = RelativePathUnsafe.UnpackPathComplex();
                var absolutePath = unpackedRelativePath.GetAbsolutePathFromRelativePath();
                return absolutePath.DoesPathExist() ? unpackedRelativePath : null;
            }
        }

        public override string AbsolutePath {
            get {
                var unpackedRelativePath = RelativePathUnsafe.UnpackPathComplex();
                var absolutePath = unpackedRelativePath.GetAbsolutePathFromRelativePath();
                return absolutePath.DoesPathExist() ? absolutePath : null;
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