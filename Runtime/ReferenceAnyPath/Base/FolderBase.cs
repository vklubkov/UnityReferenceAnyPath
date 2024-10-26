using System;

namespace ReferenceAnyPath {
    [Serializable]
    public abstract class FolderBase : Base {
        public override string Path => PathUnsafe.UnpackPathComplex();

#if UNITY_EDITOR
        public override string RelativePath =>
            AbsolutePathUnsafe.UnpackPathSimple().DoesFolderExist()
                ? RelativePathUnsafe.UnpackPathComplex()
                : null;

        public override string AbsolutePath {
            get {
                var unpackedAbsolutePath = AbsolutePathUnsafe.UnpackPathSimple();
                return unpackedAbsolutePath.DoesFolderExist() ? unpackedAbsolutePath : null;
            }
        }

        public override string AssetPath {
            get {
                var unpackedAssetPath = AssetPathUnsafe.UnpackPathSimple();
                return unpackedAssetPath.DoesAssetFolderExist() ? unpackedAssetPath : null;
            }
        }

        public override string RuntimePath =>
            AssetPathUnsafe.UnpackPathSimple().DoesAssetFolderExist()
                ? RuntimePathUnsafe.UnpackPathComplex()
                : null;
#endif
    }
}