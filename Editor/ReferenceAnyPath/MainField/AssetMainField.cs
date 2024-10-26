using UnityEditor;

namespace ReferenceAnyPath {
    public class AssetMainField<TLayout> : MainField<TLayout> where TLayout : AssetLayout {
        protected override bool DrawMainField() {
            var inputProperty = Property.MainProperty;
            var originalObject = inputProperty.objectReferenceValue;
            EditorGUI.BeginChangeCheck();
            EditorGUI.ObjectField(Layout.InputRect, inputProperty, Layout.MainGuiContent);
            var hasChanges = EditorGUI.EndChangeCheck();
            if (hasChanges && originalObject != null && inputProperty.objectReferenceValue == originalObject)
                return false;

            return hasChanges;
        }
    }
}