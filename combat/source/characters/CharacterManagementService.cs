using System.Linq;
using static EventSourcingDemo.Combat.Character;
using static EventSourcingDemo.Combat.Command;

namespace EventSourcingDemo.Combat
{
    public class CharacterManagementService :
        IHandler<CreateCharacter>,
        IHandler<RenameCharacter>,
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

            if (result.WasFailure)
                return result.Error;

            var stream = result.Value;

            if (stream.Any(x => x.EntityId == command.EntityId))
                return CannotDuplicateCharacter();

            return _store.Push(
                new CharacterCreated(
                    command.EntityId,
                    command.Attributes,
                    command.Name
                )
                {
                    Category = Category,
                    EntityId = command.EntityId
                }
            );
        }

        #endregion

        #region IHandler<RenameCharacter> Implementation

        public Result Handle(RenameCharacter command) =>
            _store.Find(command.GetEntityStreamId())
                .Bind(stream => From(stream))
                .Bind(character => character.Rename(command.Name))
                .Bind(characterRenamed => _store.Push(characterRenamed));

        #endregion

        #region IHandler<SetAttributes> Implementation

        public Result Handle(SetAttributes command) =>
            _store.Find(command.GetEntityStreamId())
                .Bind(stream => From(stream))
                .Bind(character => character.Set(command.Attributes))
                .Bind(attributesSet => _store.Push(attributesSet));

        #endregion
    }
}
