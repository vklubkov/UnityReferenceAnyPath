using UnityEngine;

namespace ReferenceAnyPath {
    public interface ILayout {
        public Rect InputRect { get; }
        public Rect SelectorRect { get; }
        public Rect FoldoutRect { get; }
        public Rect InfoRect { get; }
        public Rect InfoValueRect { get; }

        public GUIStyle InfoStyle { get; }
        public GUIStyle PlaceholderStyle { get; }

        public GUIContent MainGuiContent { get; }
        public GUIContent EmptyGuiContent { get; }
        public GUIContent NoGuiContent { get; }

        public void Init(Rect position, GUIContent label);
        public void NextLine();
    }
}