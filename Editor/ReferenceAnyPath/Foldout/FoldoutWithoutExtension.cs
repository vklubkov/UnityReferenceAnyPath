namespace ReferenceAnyPath {
    public class FoldoutWithoutExtension<TLayout> : Foldout<TLayout> where TLayout : Layout {
        protected override bool DrawFoldout() {
            DrawDefaultInfo();
            return false;
        }
    }
}