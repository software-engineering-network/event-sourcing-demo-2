﻿using System.Collections.Generic;
using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;

namespace EventSourcingDemo.CombatSpec.CharacterSpec
{
    public class WhenModifyingAttributes
    {
        #region Core

        private readonly Character _character = ObjectProvider.CreateCharacter();

        #endregion

        #region Public Interface

        public static IEnumerable<object[]> GetAttributes()
        {
            yield return new object[] { new Attributes(20, 0, 20, 10, 2, 20), new Attributes(10, 0, 0, 0, 0, 0) };
        }

        #endregion

        #region Test Methods

        [Theory]
        [MemberData(nameof(GetAttributes))]
        public void ThenReturnAttributesModifiedEvent(Attributes original, Attributes delta)
        {
            _character.Set(original);

            var @event = _character.Add(delta).Value;

            @event.Delta.Should().Be(delta);
        }

        #endregion
    }
}
