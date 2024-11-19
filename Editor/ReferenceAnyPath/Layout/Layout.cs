using UnityEditor;
using UnityEngine;

namespace ReferenceAnyPath {
    public abstract class Layout : ILayout {
       // Different versions of Unity handle the elements
       // to the left of the property rect differently.
#if UNITY_6000_0_OR_NEWER
        // Here, I officially give up on Unity 6.
        // Feels like this changes every minor release, so let's just
        // use a single value and simply update it when the new change is out.
        const float _foldoutOffset = 13f;
#elif UNITY_2022_2_OR_NEWER
        const float _foldoutOffset = 15f;
#else
        const float _foldoutOffset = 0f;
#endif

        readonly static RectOffset _descriptionLabelPadding = new(5, 5, 0, 0);

        GUIContent _label;

        Rect _inputRect;
        Rect _selectorRect;
        Rect _foldoutRect;
        Rect _infoRect;
        Rect _infoValueRect;
        GUIStyle _infoStyle;
        GUIStyle _placeholderStyle;

        float InputFieldOffset => HasBuiltInSelector ? 0f : SelectorWidth;
        protected abstract float SelectorWidth { get; }
        protected abstract bool HasBuiltInSelector { get; }

        public Rect InputRect => _inputRect;
        public Rect SelectorRect => _selectorRect;
        public Rect FoldoutRect => _foldoutRect;
        public Rect InfoRect => _infoRect;
        public Rect InfoValueRect => _infoValueRect;

        public GUIStyle InfoStyle => _infoStyle ??= new(EditorStyles.helpBox) {
            padding = _descriptionLabelPadding,
            alignment = TextAnchor.MiddleLeft,
            wordWrap = false
        };

        public GUIStyle PlaceholderStyle {
            get {
                if (_placeholderStyle == null) {
                    // ReSharper disable once UseObjectOrCollectionInitializer to avoid losing settings
                    _placeholderStyle = new(EditorStyles.label) {
                        padding = _descriptionLabelPadding,
                        wordWrap = false,
                    };

                    _placeholderStyle.normal.textColor = Color.gray;
                }

                return _placeholderStyle;
            }
        }

        public GUIContent MainGuiContent => new(_label);
        public GUIContent EmptyGuiContent => new((string)null);
        public GUIContent NoGuiContent => new();

        public void Init(Rect position, GUIContent label) {
            _label = label;
            position.x += _foldoutOffset;
            position.width -= _foldoutOffset;
            InitInputRect(position);
            InitSelectorRect(position);
            InitFoldoutRect(position);
            InitInfoRect(position);
            InitInfoValueRect();
            Initialize(position, label);
        }

        protected abstract void Initialize(Rect position, GUIContent label);

        void InitInputRect(Rect position) {
            _inputRect.x = position.x;
            _inputRect.y = position.y;
            _inputRect.width = position.width - InputFieldOffset;
            _inputRect.height = EditorGUIUtility.singleLineHeight;
        }

        void InitSelectorRect(Rect position) {
            _selectorRect.x = position.x + position.width - InputFieldOffset;
            _selectorRect.y = position.y;
            _selectorRect.width = InputFieldOffset;
            _selectorRect.height = EditorGUIUtility.singleLineHeight;
        }

        void InitFoldoutRect(Rect position) {
            _foldoutRect.x = position.x - _foldoutOffset;
            _foldoutRect.y = position.y;
            _foldoutRect.width = position.width + _foldoutOffset;
            _foldoutRect.height = EditorGUIUtility.singleLineHeight;
        }

        void InitInfoRect(Rect position) {
            _infoRect.x = position.x;
            _infoRect.y = position.y;
            _infoRect.width = position.width - SelectorWidth;
            _infoRect.height = EditorGUIUtility.singleLineHeight;
        }

        void InitInfoValueRect() => _infoValueRect = EditorGUI.PrefixLabel(InfoRect, EmptyGuiContent);

        public virtual void NextLine() {
            var singleLineHeight = EditorGUIUtility.singleLineHeight;
            _inputRect.y += singleLineHeight;
            _selectorRect.y += singleLineHeight;
            _foldoutRect.y += singleLineHeight;
            _infoRect.y += singleLineHeight;
            _infoValueRect.y += singleLineHeight;
        }
    }
}