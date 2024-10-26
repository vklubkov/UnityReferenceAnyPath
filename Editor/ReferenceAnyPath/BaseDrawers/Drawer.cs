using UnityEditor;
using UnityEngine;

namespace ReferenceAnyPath {
    public abstract class Drawer<TLayout, TProperty, TValidator, TError, TMainField, TFoldout, TChanges> :
        PropertyDrawer
            where TLayout : Layout
            where TProperty : Property
            where TValidator : Validator
            where TError : Error<TValidator>
            where TMainField : MainField<TLayout>
            where TFoldout : Foldout<TLayout>
            where TChanges : Changes<TValidator> {
        readonly Color _errorColor = new(1f, 0.5f, 0.5f);

        protected abstract TLayout Layout { get; }
        protected abstract TProperty Property { get; }
        protected abstract TValidator Validator  { get; }
        protected abstract TError Error  { get; }
        protected abstract TMainField MainField  { get; }
        protected abstract TFoldout Foldout  { get; }
        protected abstract TChanges Changes  { get; }

        int TotalLines => MainField.Lines + Foldout.Lines;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent _) {
            if (property.serializedObject.isEditingMultipleObjects)
                return 0;

            var lineHeight = EditorGUIUtility.singleLineHeight;
            return property.isExpanded ? TotalLines * lineHeight : lineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            if (property.serializedObject.isEditingMultipleObjects)
                return;

            Layout.Init(position, label);
            Property.Init(property);
            Validator.Init(Property);
            Error.Init(Property, Validator);
            MainField.Init(Layout, Property, Validator);
            Foldout.Init(Layout, Property);
            Changes.Init(Property, Validator);

            using var __ = new ColorScope(_errorColor, condition: Error.IsError);
            var hasChanges = MainField.Draw();
            hasChanges |= Foldout.Draw();
            if (hasChanges)
                Changes.Apply();

            Property.Dispose();
        }
    }
}