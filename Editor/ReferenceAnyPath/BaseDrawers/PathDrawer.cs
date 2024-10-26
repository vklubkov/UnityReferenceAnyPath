namespace ReferenceAnyPath {
    public abstract class PathDrawer<TLayout, TValidator, TError, TMainField, TFoldout, TChanges> :
        Drawer<TLayout, PathProperty, TValidator, TError, TMainField, TFoldout, TChanges>
            where TLayout : Layout
            where TValidator : Validator
            where TError : Error<TValidator>
            where TMainField : MainField<TLayout>
            where TFoldout : Foldout<TLayout>
            where TChanges : Changes<TValidator> {
        protected override PathProperty Property { get; } = new();
    }
}