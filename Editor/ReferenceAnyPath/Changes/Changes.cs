namespace ReferenceAnyPath {
    public abstract class Changes<TValidator> where TValidator : Validator {
        protected Property Property { get; private set; }
        protected TValidator Validator { get; private set; }

        public void Init(Property property, TValidator validator) {
            Property = property;
            Validator = validator;
        }

        public abstract void Apply();
    }
}