namespace ReferenceAnyPath {
    public class MainFieldWithFolderSelector<TLayout> :
        MainFieldWithSingleSelector<TLayout> where TLayout : SingleCustomSelectorLayout {
        protected override OpenDialog OpenDialog { get; } = new OpenFolderDialog();
    }
}