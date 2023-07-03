using System.Collections.Generic;
using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;
using static EventSourcingDemo.Combat.Character;
using static EventSourcingDemo.CombatSpec.Characters.ObjectProvider;

namespace EventSourcingDemo.CombatSpec.Characters.CharacterSpec
{
    public class WhenModifyingAttributes
    {
        #region Setup

        private readonly Character _character;

        public WhenModifyingAttributes()
        {
            _character = CreateCharacter();
            _character.Set(new Attributes(20, 0, 20, 10, 2, 20));
        }

        #endregion

        #region Implementation

        public static IEnumerable<object[]> GetAttributes()
        {
            yield return new object[] { new Attributes(10, 0, 0, 0, 0, 0) };
            yield return new object[] { new Attributes(-5, 10, 0, 0, 0, 0) };
        }

        #endregion

        #region Requirements

        [Fact]
        public void GivenNoDelta_ThenReturnNoOpError()
        {
            var error = _character.Add(Attributes.Default).Error;

            error.Should().Be(NoOp());
        }

        [Fact]
        public void ThenReducerAddsAttributes()
        {
            var character = CreateCharacter();

            character = Apply(character, new AttributesModified(new(10, 0, 0, 0, 0, 0)));

            character.Attributes.Should().Be(new Attributes(30, 0, 20, 10, 2, 20));
        }

        [Theory]
        [MemberData(nameof(GetAttributes))]
        public void ThenReturnAttributesModifiedEvent(Attributes delta)
        {
            var @event = _character.Add(delta).Value;

            @event.Delta.Should().Be(delta);
        }

        #endregion
    }
}
