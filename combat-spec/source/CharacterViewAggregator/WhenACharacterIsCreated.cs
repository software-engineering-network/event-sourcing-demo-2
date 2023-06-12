using System.Collections.Generic;
using EventSourcingDemo.Combat.CharacterView;
using FluentAssertions;
using Xunit;

namespace EventSourcingDemo.CombatSpec.CharacterViewAggregator
{
    public class WhenACharacterIsCreated
    {
        #region Requirements

        [Fact]
        public void ThenCharacterIsAddedToView()
        {
            var store = new MockEventStore();
            var viewRepository = new MockViewRepository();

            var aggregator = new Aggregator(store, viewRepository);
            aggregator.Start();

            var @event = new CharacterCreated("Mario");

            aggregator.Handle(@event);

            var view = (CharacterView) viewRepository.Find("CharacterView").Value;

            var expected = new HashSet<Character> { new(@event.EntityId, "Mario") };

            view.Characters.Should().BeEquivalentTo(expected);
        }

        #endregion
    }
}
