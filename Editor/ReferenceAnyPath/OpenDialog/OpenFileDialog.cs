using UnityEditor;

namespace ReferenceAnyPath {
    public class OpenFileDialog : OpenDialog {
        const string _openFileDialogTitle = "Select file";

        protected override string OpenSystemDialog(string defaultPath) =>
            EditorUtility.OpenFilePanel(_openFileDialogTitle, defaultPath, string.Empty);
    }
}