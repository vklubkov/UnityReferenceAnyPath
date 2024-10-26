using UnityEditor;
using UnityEngine;

namespace ReferenceAnyPath {
    public abstract class MainFieldWithSingleSelector<TLayout> :
        MainField<TLayout> where TLayout : SingleSelectorLayout {
        const string _selectorLabel = "...";

        readonly DropArea _dropArea = new();

        protected abstract OpenDialog OpenDialog { get; }

        public override void Init(TLayout layout, Property property, Validator validator) {
            base.Init(layout, property, validator);
            OpenDialog.Init(property.Root);
        }

        protected override bool DrawMainField() {
            var inputProperty = Property.MainProperty;
            var originalPath = inputProperty.stringValue;
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(Layout.InputRect, inputProperty, Layout.MainGuiContent);
            var hasChanges = EditorGUI.EndChangeCheck();

            if (OpenDialog.HasResult) {
                inputProperty.stringValue = OpenDialog.Result;
                OpenDialog.Consume();
                hasChanges = true;
            }

            var dropPath = _dropArea.DetectDrop(Layout, Validator);
            if (dropPath != null) {
                inputProperty.stringValue = dropPath;
                hasChanges = true;
            }

            if (GUI.Button(Layout.SelectorRect, _selectorLabel)) {
                var absolutePath = Property.GetString(PropertyName._absolutePath).UnpackPathSimple();
                OpenDialog.Open(absolutePath);
            }

            if (hasChanges && inputProperty.stringValue == originalPath)
                return false;

            return hasChanges;
        }
    }
}