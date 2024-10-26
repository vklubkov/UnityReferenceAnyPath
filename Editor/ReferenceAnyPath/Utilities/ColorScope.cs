using System;
using UnityEngine;

namespace ReferenceAnyPath {
    public readonly struct ColorScope : IDisposable {
        readonly Color _originalColor;
        readonly bool _condition;

        public ColorScope(Color color, bool condition) {
            _condition = condition;
            _originalColor = GUI.color;
            if (_condition)
                GUI.color = color;
        }

        public void Dispose() {
            if (_condition)
                GUI.color = _originalColor;
        }
    }
}