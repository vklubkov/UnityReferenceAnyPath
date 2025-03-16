using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ReferenceAnyPath {
    public abstract class OpenDialog {
        readonly static Dictionary<string, string> _results = new();

        string _propertyId;

        public string Result => _results.GetValueOrDefault(_propertyId);
        public bool HasResult => Result != null;

        public void Init(SerializedProperty property) => _propertyId = property.GetPropertyId();

        public void Open(string absolutePath) {
            var defaultPath = GetOpenDialogDefaultPath(absolutePath);
            var propertyId = _propertyId;
            EditorApplication.delayCall += () => {
                var result = OpenSystemDialog(defaultPath);
                if (string.IsNullOrEmpty(result))
                    return;

                _results[propertyId] = Path.GetRelativePath(Application.dataPath, result);

                var focusedWindow = EditorWindow.focusedWindow;
                if (focusedWindow != null)
                    EditorWindow.focusedWindow.Repaint();
            };
        }

        static string GetOpenDialogDefaultPath(string absolutePath) {
            if (Directory.Exists(absolutePath))
                return absolutePath;

            var directoryPath = Path.GetDirectoryName(absolutePath);
            if (string.IsNullOrEmpty(directoryPath))
                return Application.dataPath;

            if (File.Exists(absolutePath))
                return directoryPath;

            directoryPath = Path.GetFullPath(directoryPath).ApplyUnitySeparators();
            var projectPath = Path.GetDirectoryName(Application.dataPath).ApplyUnitySeparators();
            while (directoryPath?.StartsWith(projectPath) ?? false) {
                if (Directory.Exists(directoryPath))
                    return directoryPath;

                directoryPath = Path.GetDirectoryName(directoryPath).ApplyUnitySeparators();
            }

            return Application.dataPath;
        }

        protected abstract string OpenSystemDialog(string defaultPath);
        public void Consume() => _results.Remove(_propertyId);
    }
}