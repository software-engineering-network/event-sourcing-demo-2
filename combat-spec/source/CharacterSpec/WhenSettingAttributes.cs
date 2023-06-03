using System.Collections.Generic;
using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;
using static EventSourcingDemo.Combat.Character;
using static EventSourcingDemo.CombatSpec.ObjectProvider;

namespace EventSourcingDemo.CombatSpec.CharacterSpec
{
    public class WhenSettingAttributes
    {
        #region Core

        private readonly Character _character = CreateCharacter();

        #endregion

        #region Public Interface

        public static IEnumerable<object[]> GetSetAttributes()
        {
            yield return new object[] { new Attributes(1, 2, 3, 4, 5, 6) };
            yield return new object[] { new Attributes(2, 3, 4, 5, 6, 7) };
        }

        #endregion

        #region Test Methods

        [Fact]
        public void GivenIdenticalAttributes_ThenReturnNoOpError()
        {
            var error = _character.Set(new Attributes(20, 0, 20, 10, 2, 20)).Error;

            error.Should().Be(NoOp());
        }

        [Fact]
        public void ThenReducerSetsAttributes()
        {
            var attributes = new Attributes(10, 1, 5, 8, 15, 6);
            var character = From(
                GetCharacterCreated(),
                new AttributesSet(attributes)
            ).Value;

            character.Attributes.Should().Be(attributes);
        }

        [Theory]
        [MemberData(nameof(GetSetAttributes))]
        public void ThenReturnAttributesSetEvent(Attributes attributes)
        {
            var @event = _character.Set(attributes).Value;

            @event.Attributes.Should().Be(attributes);
        }

        #endregion
    }
}
