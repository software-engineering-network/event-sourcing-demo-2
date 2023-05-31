using System;
using static EventSourcingDemo.Combat.Character;
using static EventSourcingDemo.Combat.Result;

namespace EventSourcingDemo.Combat
{
    public class CharacterManagementService :
        ICommandHandler<CreateCharacter>,
        ICommandHandler<SetAttributes>
    {
        private readonly IEventStore _store;

        #region Creation

        public CharacterManagementService(IEventStore store)
        {
            _store = store;
        }

        #endregion

        #region Private Interface

        private Character Find(string streamId) => ;

        #endregion

        #region ICommandHandler<CreateCharacter> Implementation

        public Result Handle(CreateCharacter command)
        {
            /*
             * check category stream for existing characters;
             * a character in this application, is a character definition
             * as opposed to an instance of a character;
             */
            var @event = new CharacterCreated(Attributes.Default, Guid.NewGuid(), command.Name);

            _store.Push(@event.CharacterId, @event);

            return Success();
        }

        #endregion

        #region ICommandHandler<SetAttributes> Implementation

        public Result Handle(SetAttributes command)
        {
            var (id, characterId, attributes) = command;
            var entityStreamId = $"character-{characterId}";

            return _store.GetStream(entityStreamId)
                .Bind(stream => From(stream))
                .Bind(character => character.Set(attributes))
                .Bind(attributesSetEvent => _store.Push(entityStreamId, attributesSetEvent));
        }

        #endregion
    }
}
