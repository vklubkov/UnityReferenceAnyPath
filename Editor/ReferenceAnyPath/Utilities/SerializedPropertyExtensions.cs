using UnityEditor;

namespace ReferenceAnyPath {
    public static class SerializedPropertyExtensions {
        public static string GetPropertyId(this SerializedProperty property) =>
            $"{property.serializedObject.targetObject.GetInstanceID()}_{property.propertyPath}";

    }
}