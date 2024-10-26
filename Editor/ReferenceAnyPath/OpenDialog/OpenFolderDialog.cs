using UnityEditor;

namespace ReferenceAnyPath {
    public class OpenFolderDialog : OpenDialog {
        const string _openFolderDialogTitle = "Select folder";

        protected override string OpenSystemDialog(string defaultPath) =>
            EditorUtility.OpenFolderPanel(_openFolderDialogTitle, defaultPath, string.Empty);
    }
}