using System.IO;
using UnityEditor;
using UnityEngine;

namespace ReferenceAnyPath {
    public class DropArea {
        public string DetectDrop(Layout layout, Validator validator) {
            var currentEvent = Event.current;
            var dropAreaRect = layout.InputRect;
            if (currentEvent.type != EventType.DragUpdated &&
                currentEvent.type != EventType.DragPerform) {
                return null;
            }

            if (!dropAreaRect.Contains(currentEvent.mousePosition) || DragAndDrop.paths.Length != 1)
                return null;

            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
            if (currentEvent.type != EventType.DragPerform)
                return null;

            var path = DragAndDrop.paths[0];
            var isAbsolutePath = Path.IsPathRooted(path);

            var isValid = isAbsolutePath
                ? validator.IsValidPath(validator.Extensions, path)
                : validator.IsValidAsset(validator.Extensions, path);

            if (!isValid)
                return null;

            DragAndDrop.AcceptDrag();

           return isAbsolutePath
                ? path.GetRelativePathFromAbsolutePath()
                : path.GetRelativePathFromAssetPath();
        }
    }
}