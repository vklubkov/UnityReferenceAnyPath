using System;
using UnityEditor;

namespace ReferenceAnyPath {
    public readonly struct TemporaryIndentLevel : IDisposable {
        readonly int _originalIndent;

        public TemporaryIndentLevel(int indentLevel) {
            _originalIndent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = indentLevel;
        }

        public void Dispose() => EditorGUI.indentLevel = _originalIndent;
    }
}