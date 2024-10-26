using UnityEngine;

namespace ReferenceAnyPath {
    public class RawHeightmapAssetLayout : AssetLayout, IRawHeightmapLayout {
        readonly RawHeightmapLayoutHelper _rawHeightmapLayout = new();

        public Rect WidthLabelRect => _rawHeightmapLayout.WidthLabelRect;
        public Rect WidthValueRect => _rawHeightmapLayout.WidthValueRect;
        public Rect HeightLabelRect => _rawHeightmapLayout.HeightLabelRect;
        public Rect HeightValueRect => _rawHeightmapLayout.HeightValueRect;
        public Rect BitsLabelRect => _rawHeightmapLayout.BitsLabelRect;
        public Rect BitsValueRect => _rawHeightmapLayout.BitsValueRect;
        public Rect ByteOrderLabelRect => _rawHeightmapLayout.ByteOrderLabelRect;
        public Rect ByteOrderValueRect=> _rawHeightmapLayout.ByteOrderValueRect;
        public Rect FlipLabelRect => _rawHeightmapLayout.FlipLabelRect;
        public Rect FlipValueRect=> _rawHeightmapLayout.FlipValueRect;

        public override void Init(Rect position, GUIContent label) {
            base.Init(position, label);
            _rawHeightmapLayout.Init(InfoValueRect);
        }

        public override void NextLine() {
            _rawHeightmapLayout.NextLine();
            base.NextLine();
        }
    }
}