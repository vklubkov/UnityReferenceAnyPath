using UnityEngine;

namespace ReferenceAnyPath {
    public class SingleCustomSelectorLayout : SingleSelectorLayout {
        protected override bool HasBuiltInSelector => false;
        protected override void Initialize(Rect position, GUIContent label) { }
    }
}