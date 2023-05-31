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

        #region ICommandHandler<CreateCharacter> Implementation

        public Result Handle(CreateCharacter command)
        {
            /*
             * check component stream for existing characters;
             * a character in this application, is a character definition
             * as opposed to an instance of a character;
             */
            var @event = new CharacterCreated(Attributes.Default, Guid.NewGuid(), command.Name);

            _store.Push(@event.CharacterId, @event);

            return Success();
        }

        #endregion

        #region ICommandHandler<SetAttributes> Implementation

        public Result Handle(SetAttributes command) =>
            From(_store.GetStream(command.CharacterId))
                .Set(command.Attributes)
                .Bind(attributesSet => _store.Push(command.CharacterId, attributesSet));

        #endregion
    }
}
