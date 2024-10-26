namespace ReferenceAnyPath {
    public class FoldoutWithSceneName<TLayout> : FoldoutWithExtensionInfo<TLayout> where TLayout : AssetLayout {
        const string _sceneNamePrefix = "Scene name:     ";
        public override int Lines => base.Lines + 1;

        protected override bool DrawFoldout() {
            DrawExtensionInfo();
            DrawDefaultInfo();
            DrawSceneNameInfo();
            return false;
        }

        void DrawSceneNameInfo() {
            var name = Property.GetString(PropertyName._name);
            DrawInfo(_sceneNamePrefix, name);
        }
    }
}