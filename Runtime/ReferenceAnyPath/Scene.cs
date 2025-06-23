using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityObject = UnityEngine.Object;

namespace ReferenceAnyPath {
    [Serializable]
    public class Scene : Base, IScene {
#if UNITY_EDITOR
#pragma warning disable 0414 // Used in PropertyDrawer
        [SerializeField] string _extensions = "unity";
#pragma warning restore 0414

        [SerializeField] UnityObject _object;
#endif

        [SerializeField] string _name;

        public string Name => _name;

        public override string Path => PathUnsafe.UnpackPathSimple();

#if UNITY_EDITOR
        public override string RelativePath {
            get {
                var unpackedRelativePath = RelativePathUnsafe.UnpackPathSimple();
                var unpackedAbsolutePath = unpackedRelativePath.GetAbsolutePathFromRelativePath();
                return unpackedAbsolutePath.DoesFileExist() ? unpackedRelativePath : null;
            }
        }

        public override string AbsolutePath {
            get {
                var unpackedRelativePath = RelativePathUnsafe.UnpackPathSimple();
                var unpackedAbsolutePath = unpackedRelativePath.GetAbsolutePathFromRelativePath();
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

        protected override void OnBeforeSerialize(
            ref string path,
            ref string relativePath,
            ref string assetPath,
            ref string runtimePath,
            ref bool error) =>
            SceneHelper.OnBeforeSerialize(
                _object,
                ref path,
                ref relativePath,
                ref assetPath,
                ref runtimePath,
                ref _name,
                ref error,
                getRuntimePath: StringExtensions.GetScenePathFromAssetPath);
#endif

        // Don't check the path for null here to not hide any errors.

        public void LoadScene() => SceneManager.LoadScene(Path);
        public void LoadScene(LoadSceneMode mode) => SceneManager.LoadScene(Path, mode);
        public void LoadScene(LoadSceneParameters parameters) => SceneManager.LoadScene(Path, parameters);

        // Note: AsyncOperation can be set to null by Unity

        public AsyncOperation LoadSceneAsync() => SceneManager.LoadSceneAsync(Path);
        public AsyncOperation LoadSceneAsync(LoadSceneMode mode) => SceneManager.LoadSceneAsync(Path, mode);

        public AsyncOperation LoadSceneAsync(LoadSceneParameters parameters) =>
            SceneManager.LoadSceneAsync(Path, parameters);
    }
}