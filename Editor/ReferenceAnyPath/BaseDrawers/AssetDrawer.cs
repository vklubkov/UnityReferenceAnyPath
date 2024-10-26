namespace ReferenceAnyPath {
    public abstract class AssetDrawer<TLayout, TValidator, TError, TFoldout, TChanges> :
        Drawer<
            TLayout,
            ObjectProperty,
            TValidator,
            TError,
            AssetMainField<TLayout>,
            TFoldout,
            TChanges>
        where TLayout : AssetLayout
        where TValidator : Validator
        where TError : Error<TValidator>
        where TFoldout : Foldout<TLayout>
        where TChanges : Changes<TValidator> {
        protected override ObjectProperty Property { get; } = new();
        protected override AssetMainField<TLayout> MainField { get; } = new();
    }
}