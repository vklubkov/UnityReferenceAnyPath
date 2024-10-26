using System;
using UnityEngine;

namespace ReferenceAnyPath {
    [Serializable]
    public abstract class FileBase : Base {
        public override string Path => PathUnsafe.UnpackPathSimple();

#if UNITY_EDITOR
        // ReSharper disable UnusedMember.Local because it is used in PropertyDrawer
        [SerializeField] string _extensions = string.Empty;
        // ReSharper restore UnusedMember.Local

        public override string RelativePath =>
            AbsolutePathUnsafe.UnpackPathSimple().DoesFileExist()
                ? RelativePathUnsafe.UnpackPathSimple()
                : null;

        public override string AbsolutePath {
            get {
                var unpackedAbsolutePath = AbsolutePathUnsafe.UnpackPathSimple();
                return unpackedAbsolutePath.DoesFileExist() ? unpackedAbsolutePath : null;
            }
        }

        public override string AssetPath {
            get {
                var unpackedAssetPath = AssetPathUnsafe.UnpackPathSimple();
                return unpackedAssetPath.DoesAssetFileExist() ? unpackedAssetPath : null;
            }
        }

        public override string RuntimePath =>
            AssetPathUnsafe.UnpackPathSimple().DoesAssetFileExist()
                ? RuntimePathUnsafe.UnpackPathSimple()
                : null;
#endif
    }
}