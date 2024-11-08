using UnityEditor;
using UnityEngine;

namespace ReferenceAnyPath {
    public class MainFieldWithFileAndFolderSelectors<TLayout> :
        MainField<TLayout> where TLayout : TwoCustomSelectorsLayout {
        const string _fileSelectorLabel = "*.*";
        const string _folderSelectorLabel = "*";

        readonly OpenFileDialog _openFileDialog = new();
        readonly OpenFolderDialog _openFolderDialog = new();
        readonly DropArea _dropArea = new();

        public override void Init(TLayout layout, Property property, Validator validator) {
            base.Init(layout, property, validator);
            _openFileDialog.Init(property.Root);
            _openFolderDialog.Init(property.Root);
        }

        protected override bool DrawMainField() {
            var inputProperty = Property.MainProperty;
            var originalPath = inputProperty.stringValue;
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(Layout.InputRect, inputProperty, Layout.MainGuiContent);
            var hasChanges = EditorGUI.EndChangeCheck();

            if (_openFileDialog.HasResult) {
                inputProperty.stringValue = _openFileDialog.Result;
                _openFileDialog.Consume();
                hasChanges = true;
            }

            if (_openFolderDialog.HasResult) {
                inputProperty.stringValue = _openFolderDialog.Result;
                _openFolderDialog.Consume();
                hasChanges = true;
            }

            var dropPath = _dropArea.DetectDrop(Layout, Validator);
            if (dropPath != null) {
                inputProperty.stringValue = dropPath;
                hasChanges = true;
            }

            if (GUI.Button(Layout.FileSelectorRect, _fileSelectorLabel)) {
                var unpackedRelativePath = Property.GetString(PropertyName._relativePath).UnpackPathSimple();
                var absolutePath = unpackedRelativePath.GetAbsolutePathFromRelativePath();
                _openFileDialog.Open(absolutePath);
            }

            if (GUI.Button(Layout.FolderSelectorRect, _folderSelectorLabel)) {
                var unpackedRelativePath = Property.GetString(PropertyName._relativePath).UnpackPathSimple();
                var absolutePath = unpackedRelativePath.GetAbsolutePathFromRelativePath();
                _openFolderDialog.Open(absolutePath);
            }

            if (hasChanges && inputProperty.stringValue == originalPath)
                return false;

            return hasChanges;
        }
    }
}