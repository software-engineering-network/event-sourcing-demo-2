using System.Collections.Generic;
using EventSourcingDemo.Combat.CharacterView;
using FluentAssertions;
using Xunit;

namespace EventSourcingDemo.CombatSpec.CharacterViewAggregator
{
    public class WhenACharacterIsCreated
    {
        #region Setup

        private readonly Aggregator _aggregator;
        private readonly MockEventStore _store = new();
        private readonly MockViewRepository _viewRepository = new();

        public WhenACharacterIsCreated()
        {
            _aggregator = new Aggregator(_store, _viewRepository);
            _aggregator.Start();
        }

        #endregion

        #region Requirements

        [Fact]
        public void ThenCharacterIsAddedToView()
        {
            CharacterCreated @event;

            _aggregator.Handle(@event = new CharacterCreated("Mario"));

            var view = (CharacterView) _viewRepository.Find("CharacterView").Value;
            var expected = new List<Character> { new(@event.EntityId, "Mario") };

            view.Characters.Should().BeEquivalentTo(expected);
        }

        #endregion

        public class GivenTheEventIsProcessed
        {
            #region Setup

            private readonly Aggregator _aggregator;
            private readonly MockEventStore _store = new();
            private readonly MockViewRepository _viewRepository = new();

            public GivenTheEventIsProcessed()
            {
                _aggregator = new Aggregator(_store, _viewRepository);
                _aggregator.Start();
            }

            #endregion

            #region Requirements

            [Fact]
            public void ThenNoOp()
            {
                CharacterCreated @event;

                _aggregator.Handle(@event = new CharacterCreated("Mario"));
                _aggregator.Handle(@event);

                var view = (CharacterView) _viewRepository.Find("CharacterView").Value;
                var expected = new List<Character> { new(@event.EntityId, "Mario") };

                view.Characters.Should().BeEquivalentTo(expected);
            }

            #endregion
        }
    }
}
