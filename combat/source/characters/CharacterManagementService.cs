using System;
using System.Collections.Generic;
using System.Linq;
using static EventSourcingDemo.Combat.Character;
using static EventSourcingDemo.Combat.Command;

namespace EventSourcingDemo.Combat
{
    public class CharacterManagementService :
        IHandler<CreateCharacter>,
        IHandler<ModifyAttributes>,
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

            var characters = CreateCharacters(stream);

            if (characters.Any(x => x.Name == command.Name))
                return CharacterAlreadyExists();

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

        #region IHandler<ModifyAttributes> Implementation

        public Result Handle(ModifyAttributes command) => throw new NotImplementedException();

        #endregion

        #region IHandler<RenameCharacter> Implementation

        public Result Handle(RenameCharacter command) =>
            _store.Find(command.GetEntityStreamId())
                .Bind(Rehydrate)
                .Bind(character => character.Rename(command.Name))
                .Bind(characterRenamed => _store.Push(characterRenamed));

        #endregion

        #region IHandler<SetAttributes> Implementation

        public Result Handle(SetAttributes command) =>
            _store.Find(command.GetEntityStreamId())
                .Bind(Rehydrate)
                .Bind(character => character.Set(command.Attributes))
                .Bind(attributesSet => _store.Push(attributesSet));

        #endregion

        #region Static Interface

        private static IEnumerable<Character> CreateCharacters(IEnumerable<Event> stream)
        {
            var entityStreamIds = stream.Select(x => new StreamId(Category, x.EntityId)).Distinct();

            return entityStreamIds.Select(id => Rehydrate(stream.Where(x => x.IsInEntity(id)).ToArray()).Value);
        }

        #endregion
    }
}
