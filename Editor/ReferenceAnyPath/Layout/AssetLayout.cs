using UnityEngine;

namespace ReferenceAnyPath {
    public class AssetLayout : SingleSelectorLayout {
        protected override bool HasBuiltInSelector => true;
        protected override void Initialize(Rect position, GUIContent label) { }
    }
}