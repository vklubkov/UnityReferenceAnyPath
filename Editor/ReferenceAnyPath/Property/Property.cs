using System;
using System.Collections.Generic;
using UnityEditor;
using UnityObject = UnityEngine.Object;

namespace ReferenceAnyPath {
    public abstract class Property : IDisposable {
        readonly string _mainPropertyName;
        readonly Dictionary<PropertyName, SerializedProperty> _properties = new();

        SerializedObject _temporaryObject;
        public SerializedProperty Root { get; private set; }
        public SerializedProperty MainProperty { get; private set; }
        public bool IsError { get; private set; }

        protected Property(string mainPropertyName) => _mainPropertyName = mainPropertyName;

        public void Init(SerializedProperty property) {
            _properties.Clear();
            Root = property;

            var originalProperty = property.FindPropertyRelative(_mainPropertyName);
            _temporaryObject = new SerializedObject(property.serializedObject.targetObject);
            MainProperty = _temporaryObject.FindProperty(originalProperty.propertyPath);

            IsError = ResolveProperty(PropertyName._error).boolValue;
        }

        public SerializedProperty GetProperty(PropertyName propertyName) => ResolveProperty(propertyName);
        public string GetString(PropertyName propertyName) => ResolveProperty(propertyName).stringValue;
        public int? GetInt(PropertyName propertyName) => ResolveProperty(propertyName).intValue;

        public void SetString(PropertyName propertyName, string value) =>
            ResolveProperty(propertyName).stringValue = value;

        public void SetObject(PropertyName propertyName, UnityObject value) =>
            ResolveProperty(propertyName).objectReferenceValue = value;

        SerializedProperty ResolveProperty(PropertyName propertyName) {
            return _properties.TryGetValue(propertyName, out var property)
                ? property
                : _properties[propertyName] = Root.FindPropertyRelative(propertyName.ToString());
        }

        public void Dispose() => _temporaryObject.Dispose();
    }
}