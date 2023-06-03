using System;

namespace EventSourcingDemo.Combat
{
    public partial class Character
    {
        #region Creation

        private Character(Character source, string name) : this(source)
        {
            Name = name;
        }

        #endregion

        #region Public Interface

        public Result<CharacterRenamed> Rename(string name) => new CharacterRenamed(StreamId, name);

        #endregion

        #region Static Interface

        private static Character Apply(Character target, CharacterRenamed @event) => new(target, @event.Name);

        #endregion
    }

    public record RenameCharacter : Command
    {
        #region Creation

        public RenameCharacter(Guid id, StreamId streamId, string name) : base(id, streamId)
        {
            Name = name;
        }

        public RenameCharacter(StreamId streamId, string name) : base(streamId)
        {
            Name = name;
        }

        public RenameCharacter(string category, string name) : base(category)
        {
            Name = name;
        }

        #endregion

        #region Public Interface

        public string Name { get; init; }

        public void Deconstruct(out StreamId entityStreamId, out string name)
        {
            entityStreamId = GetEntityStreamId();
            name = Name;
        }

        #endregion
    }

    public record CharacterRenamed : Event
    {
        #region Creation

        public CharacterRenamed(StreamId streamId, string name) : base(streamId)
        {
            Name = name;
        }

        #endregion

        #region Public Interface

        public string Name { get; init; }

        #endregion
    }
}
