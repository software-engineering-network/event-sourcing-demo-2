﻿using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;
using static EventSourcingDemo.Combat.Character;
using static EventSourcingDemo.Combat.CharacterManagementService;

namespace EventSourcingDemo.CombatSpec.CharacterSpec
{
    public class WhenCreating
    {
        #region Test Methods

        [Fact(Skip = "move to service")]
        public void ThenEntityIdIsCommandEntityId()
        {
            var streamId = new StreamId(Category);
            var character = From(
                new CharacterCreated(
                    streamId.EntityId,
                    Attributes.Default,
                    "Mario"
                )
            ).Value;

            character.Id.Should().Be(streamId.EntityId);
        }

        #endregion
    }
}
