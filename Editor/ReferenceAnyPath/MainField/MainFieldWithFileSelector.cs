namespace ReferenceAnyPath {
    public class MainFieldWithFileSelector<TLayout> :
        MainFieldWithSingleSelector<TLayout> where TLayout : SingleSelectorLayout {
        protected override OpenDialog OpenDialog { get; } = new OpenFileDialog();
    }
}