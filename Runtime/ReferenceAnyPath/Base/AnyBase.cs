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

        public override string RelativePath =>
            AbsolutePathUnsafe.UnpackPathSimple().DoesPathExist()
                ? RelativePathUnsafe.UnpackPathComplex()
                : null;

        public override string AbsolutePath {
            get {
                var unpackedAbsolutePath = AbsolutePathUnsafe.UnpackPathSimple();
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