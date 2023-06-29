using System;
using System.Linq;
using static EventSourcingDemo.Combat.Character;
using static EventSourcingDemo.Combat.Command;
using static EventSourcingDemo.Combat.Result;

namespace EventSourcingDemo.Combat
{
    public class CharacterManagementService :
        IHandler<CreateCharacter>,
        IHandler<ModifyAttributes>,
        IHandler<RenameCharacter>,
        IHandler<SetAttributes>
    {
        public const string Category = "CharacterManagement";
        public static readonly CategoryStreamId StreamId = new(Category);
        private readonly IEventStore _store;

        #region Creation

        public CharacterManagementService(IEventStore store)
        {
            _store = store;
        }

        #endregion

        #region IHandler<CreateCharacter> Implementation

        public Result Handle(CreateCharacter command) =>
            _store.Find(StreamId)
                .Bind(stream => CheckForDuplicates(command.Name, stream))
                .Bind(
                    () => _store.Push(
                        new CharacterCreated(
                            command.EntityId,
                            command.Attributes,
                            command.Name
                        )
                        {
                            Category = Category,
                            EntityId = command.EntityId
                        }
                    )
                );

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

        private static Result CheckForDuplicates(string name, CategoryStream stream) =>
            stream.ProjectAll(Rehydrate).Any(x => x.Name == name)
                ? CharacterAlreadyExists()
                : Success();

        #endregion
    }
}
