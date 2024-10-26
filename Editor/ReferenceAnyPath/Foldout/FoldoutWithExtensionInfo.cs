namespace ReferenceAnyPath {
    public class FoldoutWithExtensionInfo<TLayout> : Foldout<TLayout> where TLayout : ILayout {
        const string _extensionPrefix = "Extension:         ";

        public override int Lines => base.Lines + 1;

        protected override bool DrawFoldout() {
            DrawExtensionInfo();
            DrawDefaultInfo();
            return false;
        }

        protected void DrawExtensionInfo() {
            var extensions = Property.GetString(PropertyName._extensions);
            DrawInfo(_extensionPrefix, extensions);
        }
    }
}