using System;

namespace ReferenceAnyPath {
    public abstract class WithoutExtensionValidator : Validator {
        public override string[] Extensions { get; } = Array.Empty<string>();
        public override void Init(Property property) { }
    }
}