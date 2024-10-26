using UnityEngine;

namespace ReferenceAnyPath {
    public interface IRawHeightmapLayout : ILayout {
        public Rect WidthLabelRect { get; }
        public Rect WidthValueRect { get; }
        public Rect HeightLabelRect { get; }
        public Rect HeightValueRect { get; }
        public Rect BitsLabelRect { get; }
        public Rect BitsValueRect { get; }
        public Rect ByteOrderLabelRect { get; }
        public Rect ByteOrderValueRect { get; }
        public Rect FlipLabelRect { get; }
        public Rect FlipValueRect { get; }
    }
}