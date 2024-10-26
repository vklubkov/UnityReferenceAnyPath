namespace ReferenceAnyPath {
    public abstract class MainField<TLayout> where TLayout : Layout {
        protected TLayout Layout { get; private set; }
        protected Property Property { get; private set; }
        protected Validator Validator { get; private set; }

        public int Lines => 1;

        public virtual void Init(TLayout layout, Property property, Validator validator) {
            Layout = layout;
            Property = property;
            Validator = validator;
        }

        public bool Draw() => DrawMainField();
        protected abstract bool DrawMainField();
    }
}