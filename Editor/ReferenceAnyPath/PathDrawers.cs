using UnityEditor;

namespace ReferenceAnyPath {
    [CustomPropertyDrawer(typeof(AnyPath), useForChildren: true)]
    public class AnyPathDrawer :
        PathDrawer<
            TwoCustomSelectorsLayout,
            AnyPathValidator,
            PathErrorWithExtension<AnyPathValidator>,
            MainFieldWithFileAndFolderSelectors<TwoCustomSelectorsLayout>,
            FoldoutWithExtensionInput<TwoCustomSelectorsLayout>,
            PathChangesWithExtension<AnyPathValidator>> {
        protected override TwoCustomSelectorsLayout Layout { get; } = new();
        protected override AnyPathValidator Validator { get; } = new();
        protected override PathErrorWithExtension<AnyPathValidator> Error { get; } = new();
        protected override MainFieldWithFileAndFolderSelectors<TwoCustomSelectorsLayout> MainField { get; } = new ();
        protected override FoldoutWithExtensionInput<TwoCustomSelectorsLayout> Foldout { get; } = new();
        protected override PathChangesWithExtension<AnyPathValidator> Changes { get; } = new();
    }

    [CustomPropertyDrawer(typeof(AnyFile), useForChildren: true)]
    public class FilePathDrawer :
        PathDrawer<
            SingleCustomSelectorLayout,
            FilePathValidator,
            PathErrorWithExtension<FilePathValidator>,
            MainFieldWithFileSelector<SingleCustomSelectorLayout> ,
            FoldoutWithExtensionInput<SingleCustomSelectorLayout>,
            PathChangesWithExtension<FilePathValidator>> {
        protected override SingleCustomSelectorLayout Layout { get; } = new();
        protected override FilePathValidator Validator { get; } = new();
        protected override PathErrorWithExtension<FilePathValidator> Error { get; } = new();
        protected override MainFieldWithFileSelector<SingleCustomSelectorLayout> MainField { get; } = new ();
        protected override FoldoutWithExtensionInput<SingleCustomSelectorLayout> Foldout { get; } = new();
        protected override PathChangesWithExtension<FilePathValidator> Changes { get; } = new();
    }

    [CustomPropertyDrawer(typeof(AnyFolder), useForChildren: true)]
    public class FolderPathDrawer :
        PathDrawer<
            SingleCustomSelectorLayout,
            FolderPathValidator,
            PathErrorWithoutExtension<FolderPathValidator>,
            MainFieldWithFolderSelector<SingleCustomSelectorLayout>,
            FoldoutWithoutExtension<SingleCustomSelectorLayout>,
            PathChangesWithoutExtension<FolderPathValidator>> {
        protected override SingleCustomSelectorLayout Layout { get; } = new();
        protected override FolderPathValidator Validator { get; } = new();
        protected override PathErrorWithoutExtension<FolderPathValidator> Error { get; } = new();
        protected override MainFieldWithFolderSelector<SingleCustomSelectorLayout> MainField { get; } = new();
        protected override FoldoutWithoutExtension<SingleCustomSelectorLayout> Foldout { get; } = new();
        protected override PathChangesWithoutExtension<FolderPathValidator> Changes { get; } = new();
    }
}