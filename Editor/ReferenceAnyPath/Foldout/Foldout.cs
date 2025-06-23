using UnityEditor;
using UnityEngine;

namespace ReferenceAnyPath {
    public abstract class Foldout<TLayout> where TLayout : ILayout {
        const string _absolutePathPrefix = "Absolute path:   ";
        const string _relativePathPrefix = "Relative path:    ";
        const string _assetPathPrefix = "Asset path:        ";
        const string _runtimePathPrefix = "Runtime path:    ";
        const string _null = "<null>";

        protected TLayout Layout { get; private set; }
        protected Property Property { get; private set; }

        public virtual int Lines => 4;

        public void Init(TLayout layout, Property property) {
            Layout = layout;
            Property = property;
        }

        public bool Draw() {
            if (!IsFoldout())
                return false;

            return DrawFoldout();
        }

        bool IsFoldout() {
            var foldoutRect = Layout.FoldoutRect;
            var propertyRoot = Property.Root;

            var isUnfolded = propertyRoot.isExpanded;
            propertyRoot.isExpanded = EditorGUI.Foldout(foldoutRect, isUnfolded, string.Empty);

            var currentEvent = Event.current;
            if (currentEvent.type == EventType.MouseDown &&
                foldoutRect.Contains(currentEvent.mousePosition)) {
                propertyRoot.isExpanded = !isUnfolded;
                currentEvent.Use();
            }

            return isUnfolded;
        }

        protected abstract bool DrawFoldout();

        protected void DrawDefaultInfo() {
            var unpackedRelativePath = Property.GetString(PropertyName._relativePath).UnpackPathComplex();
            var unpackedAbsolutePath = unpackedRelativePath.GetAbsolutePathFromRelativePath();
            DrawInfo(_relativePathPrefix, unpackedRelativePath);
            DrawInfo(_absolutePathPrefix, unpackedAbsolutePath);
            DrawInfo(_assetPathPrefix, Property.GetString(PropertyName._assetPath).UnpackPathSimple());
            DrawInfo(_runtimePathPrefix, Property.GetString(PropertyName._runtimePath).UnpackPathComplex());
        }

        protected void DrawInfo(string prefix, string message) {
            Layout.NextLine();
            EditorGUI.LabelField(Layout.InfoRect, null, prefix + (message ?? _null), Layout.InfoStyle);
        }
    }
}