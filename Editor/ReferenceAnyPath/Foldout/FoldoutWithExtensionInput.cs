using UnityEditor;

namespace ReferenceAnyPath {
    public class FoldoutWithExtensionInput<TLayout> : Foldout<TLayout> where TLayout : ILayout {
        const string _extensionsPlaceholder = "Extensions, e.g. \"jpg,png, tga tif,  psd\"";

        public override int Lines => base.Lines + 1;

        protected override bool DrawFoldout() {
            var hasChanges = DrawExtensionsInputField();
            DrawDefaultInfo();
            return hasChanges;
        }

        protected bool DrawExtensionsInputField() {
            Layout.NextLine();
            var extensionsProperty = Property.GetProperty(PropertyName._extensions);
            var originalExtensions = extensionsProperty.stringValue;
            EditorGUI.PropertyField(Layout.InfoRect, extensionsProperty, Layout.EmptyGuiContent);
            var hasChanges = originalExtensions != extensionsProperty.stringValue;
            if (string.IsNullOrEmpty(extensionsProperty.stringValue))
                EditorGUI.LabelField(Layout.InfoRect, null, _extensionsPlaceholder, Layout.PlaceholderStyle);

            return hasChanges;
        }
    }
}