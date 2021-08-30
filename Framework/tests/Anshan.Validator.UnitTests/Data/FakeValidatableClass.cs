using System;
using FluentValidation;

namespace Anshan.Validator.UnitTests.Data
{
    public class FakeValidatableClass
    {
        public string Text { get; set; }

        public int Number { get; set; }

        public Guid Guid { get; set; }
    }

    public class FakeValidator : AbstractValidator<FakeValidatableClass>
    {
    }
}