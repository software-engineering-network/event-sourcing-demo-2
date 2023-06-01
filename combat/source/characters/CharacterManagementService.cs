using static EventSourcingDemo.Combat.Character;
using static EventSourcingDemo.Combat.Command;

namespace EventSourcingDemo.Combat
{
    public class CharacterManagementService :
        IHandler<CreateCharacter>,
        IHandler<SetAttributes>
    {
        public const string Category = "CharacterManagement";
        private readonly IEventStore _store;

        #region Creation

        public CharacterManagementService(IEventStore store)
        {
            _store = store;
        }

        #endregion

        #region IHandler<CreateCharacter> Implementation

        public Result Handle(CreateCharacter command)
        {
            var result = _store.Find(new(Category));

            if (result.WasSuccessful)
                return CannotDuplicateCharacter();

            return _store.Push(
                new CharacterCreated(
                    new(Category, command.EntityId),
                    command.Name,
                    Attributes.Default
                )
            );
        }

        #endregion

        #region IHandler<SetAttributes> Implementation

        public Result Handle(SetAttributes command)
        {
            var (entityStreamId, attributes) = command;

            return _store.Find(entityStreamId)
                .Bind(stream => From(stream))
                .Bind(character => character.Set(attributes))
                .Bind(attributesSet => _store.Push(attributesSet));
        }

        #endregion
    }
}
