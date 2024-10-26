using System;
using System.Linq;

namespace ReferenceAnyPath {
    public abstract class WithExtensionValidator : Validator {
        Property _property;

        public override string[] Extensions {
            get {
                var extensionsString = _property.GetString(PropertyName._extensions);
                if (string.IsNullOrEmpty(extensionsString))
                    return Array.Empty<string>();

                var extensions = extensionsString.Split(',', ' ');
                if (extensions.All(string.IsNullOrWhiteSpace))
                    return Array.Empty<string>();

                return extensions;
            }
        }

        public override void Init(Property property) => _property = property;

        public bool IsValidExtension(string[] extensions, string absolutePath) {
            if (string.IsNullOrEmpty(absolutePath))
                return true;

            if (extensions.Length == 0)
                return true; // Not restricted

            var fileExtension = absolutePath.GetExtension();
            if (string.IsNullOrEmpty(fileExtension))
                return false; // Restricted, but the file has no extension

            return extensions.Contains(fileExtension);
        }
    }
}