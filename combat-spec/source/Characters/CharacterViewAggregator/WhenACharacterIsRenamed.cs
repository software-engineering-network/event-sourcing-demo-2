using System.Collections.Generic;
using EventSourcingDemo.Combat.CharacterView;
using FluentAssertions;
using Xunit;
using static System.Guid;

namespace EventSourcingDemo.CombatSpec.Characters.CharacterViewAggregator
{
    public class WhenACharacterIsRenamed
    {
        #region Setup

        private readonly List<Character> _expectedCharacters;
        private readonly MockEventStore _store = new();
        private readonly CharacterView _view;
        private readonly MockViewRepository _viewRepository = new();

        public WhenACharacterIsRenamed()
        {
            var aggregator = new Aggregator(_store, _viewRepository);
            aggregator.Start();

            var createMario = new CharacterCreated("Mario") { EntityId = NewGuid() };
            var createLuigi = new CharacterCreated("Luigi") { EntityId = NewGuid() };

            aggregator.Handle(createMario);
            aggregator.Handle(createLuigi);
            aggregator.Handle(new CharacterRenamed("Maria") { EntityId = createMario.EntityId });

            _view = (CharacterView) _viewRepository.Find("CharacterView").Value;

            _expectedCharacters = new List<Character>
            {
                new(createMario.EntityId, "Maria"),
                new(createLuigi.EntityId, "Luigi")
            };
        }

        #endregion

        #region Requirements

        [Fact]
        public void ThenTheCharacterIsRenamed() => _view.Characters.Should().BeEquivalentTo(_expectedCharacters);

        #endregion

        public class GivenTheEventIsProcessed
        {
            #region Setup

            private readonly List<Character> _expectedCharacters;
            private readonly MockEventStore _store = new();
            private readonly CharacterView _view;
            private readonly MockViewRepository _viewRepository = new();

            public GivenTheEventIsProcessed()
            {
                var aggregator = new Aggregator(_store, _viewRepository);
                aggregator.Start();

                var createMario = new CharacterCreated("Mario") { EntityId = NewGuid() };
                var createLuigi = new CharacterCreated("Luigi") { EntityId = NewGuid() };

                aggregator.Handle(createMario);
                aggregator.Handle(createLuigi);
                aggregator.Handle(new CharacterRenamed("Maria") { EntityId = createMario.EntityId });
                aggregator.Handle(new CharacterRenamed("Maria") { EntityId = createMario.EntityId });

                _view = (CharacterView) _viewRepository.Find("CharacterView").Value;

                _expectedCharacters = new List<Character>
                {
                    new(createMario.EntityId, "Maria"),
                    new(createLuigi.EntityId, "Luigi")
                };
            }

            #endregion

            #region Requirements

            [Fact]
            public void ThenNoOp() => _view.Characters.Should().BeEquivalentTo(_expectedCharacters);

            #endregion
        }
    }
}
