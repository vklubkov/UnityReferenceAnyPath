using UnityEditor;
using UnityEngine;

namespace ReferenceAnyPath {
    public class TwoCustomSelectorsLayout : Layout {
        const int _fileSelectorSize = 30;
        const int _folderSelectorSize = 20;

        Rect _fileSelectorRect;
        Rect _folderSelectorRect;

        protected override float SelectorWidth => _fileSelectorSize + _folderSelectorSize;
        protected override bool HasBuiltInSelector => false;

        public Rect FileSelectorRect => _fileSelectorRect;
        public Rect FolderSelectorRect => _folderSelectorRect;

        public override void Init(Rect position, GUIContent label) {
            base.Init(position, label);
            InitFileSelectorRect(position);
            InitFolderSelectorRect(position);
        }

        void InitFileSelectorRect(Rect position) {
            _fileSelectorRect.x = position.x + position.width - SelectorWidth;
            _fileSelectorRect.y = position.y;
            _fileSelectorRect.width = _fileSelectorSize;
            _fileSelectorRect.height = EditorGUIUtility.singleLineHeight;
        }

        void InitFolderSelectorRect(Rect position) {
            _folderSelectorRect.x = position.x + position.width - _folderSelectorSize;
            _folderSelectorRect.y = position.y;
            _folderSelectorRect.width = _folderSelectorSize;
            _folderSelectorRect.height = EditorGUIUtility.singleLineHeight;
        }

        public override void NextLine() {
            var singleLineHeight = EditorGUIUtility.singleLineHeight;
            _fileSelectorRect.y += singleLineHeight;
            _folderSelectorRect.y += singleLineHeight;
            base.NextLine();
        }
    }
}