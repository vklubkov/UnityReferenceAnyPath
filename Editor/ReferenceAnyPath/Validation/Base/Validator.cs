namespace ReferenceAnyPath {
    public abstract class Validator {
        public abstract string[] Extensions { get; }
        public abstract void Init(Property property);
        public abstract bool IsValidAsset(string[] extensions, string assetPath);

        // Note: requires to be safe to use in a background thread
        public abstract bool IsValidPath(string[] extensions, string absolutePath);
    }
}