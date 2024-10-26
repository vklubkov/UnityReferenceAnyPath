using UnityEditor;
using UnityEngine;

namespace ReferenceAnyPath {
    public class RawHeightmapLayoutHelper {
        const float _labelWidth = 70f;
        const float _centerOffset = 5f;

        Rect _widthLabelRect;
        Rect _widthValueRect;
        Rect _heightLabelRect;
        Rect _heightValueRect;
        Rect _bitsLabelRect;
        Rect _bitsValueRect;
        Rect _byteOrderLabelRect;
        Rect _byteOrderValueRect;
        Rect _flipLabelRect;
        Rect _flipValueRect;

        public Rect WidthLabelRect => _widthLabelRect;
        public Rect WidthValueRect => _widthValueRect;
        public Rect HeightLabelRect => _heightLabelRect;
        public Rect HeightValueRect => _heightValueRect;
        public Rect BitsLabelRect => _bitsLabelRect;
        public Rect BitsValueRect => _bitsValueRect;
        public Rect ByteOrderLabelRect => _byteOrderLabelRect;
        public Rect ByteOrderValueRect => _byteOrderValueRect;
        public Rect FlipLabelRect => _flipLabelRect;
        public Rect FlipValueRect => _flipValueRect;

        public void Init(Rect infoValueRect) {
            InitWidthRects(infoValueRect);
            InitHeightRects(infoValueRect);
            InitBitsRects(infoValueRect);
            InitByteOrderRects(infoValueRect);
            InitFlipRects(infoValueRect);
        }

        void InitWidthRects(Rect infoValueRect) {
            var halfWidth = infoValueRect.width / 2;
            var widthValueRect = new Rect(infoValueRect.x, infoValueRect.y, halfWidth, infoValueRect.height);
            _widthLabelRect = new Rect(widthValueRect.x, widthValueRect.y, _labelWidth, widthValueRect.height);
            widthValueRect.x += _labelWidth;
            widthValueRect.width -= _labelWidth + _centerOffset;
            _widthValueRect = widthValueRect;
        }

        void InitHeightRects(Rect infoValueRect) {
            var halfWidth = infoValueRect.width / 2;

            var heightValueRect =
                new Rect(infoValueRect.x + halfWidth, infoValueRect.y, halfWidth, infoValueRect.height);

            _heightLabelRect =
                new Rect(heightValueRect.x + _centerOffset, heightValueRect.y, _labelWidth, heightValueRect.height);

            heightValueRect.x += _labelWidth + _centerOffset;
            heightValueRect.width -= _labelWidth + _centerOffset;
            _heightValueRect = heightValueRect;
        }

        void InitBitsRects(Rect infoValueRect) {
            _bitsLabelRect = new Rect(infoValueRect.x, infoValueRect.y, _labelWidth, infoValueRect.height);
            infoValueRect.x += _labelWidth;
            infoValueRect.width -= _labelWidth;
            _bitsValueRect = infoValueRect;
        }

        void InitByteOrderRects(Rect infoValueRect) {
            _byteOrderLabelRect = new Rect(infoValueRect.x, infoValueRect.y, _labelWidth, infoValueRect.height);
            infoValueRect.x += _labelWidth;
            infoValueRect.width -= _labelWidth;
            _byteOrderValueRect = infoValueRect;
        }

        void InitFlipRects(Rect infoValueRect) {
            _flipLabelRect = new Rect(infoValueRect.x, infoValueRect.y, _labelWidth, infoValueRect.height);
            infoValueRect.x += _labelWidth;
            infoValueRect.width -= _labelWidth;
            _flipValueRect = infoValueRect;
        }

        public void NextLine() {
            var singleLineHeight = EditorGUIUtility.singleLineHeight;
            _widthLabelRect.y += singleLineHeight;
            _widthValueRect.y += singleLineHeight;
            _heightLabelRect.y += singleLineHeight;
            _heightValueRect.y += singleLineHeight;
            _bitsLabelRect.y += singleLineHeight;
            _bitsValueRect.y += singleLineHeight;
            _byteOrderLabelRect.y += singleLineHeight;
            _byteOrderValueRect.y += singleLineHeight;
            _flipLabelRect.y += singleLineHeight;
            _flipValueRect.y += singleLineHeight;
        }
    }
}