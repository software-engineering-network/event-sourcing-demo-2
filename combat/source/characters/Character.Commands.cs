using System;

namespace EventSourcingDemo.Combat
{
    public partial class Character
    {
        public record CreateCharacter : Command
        {
            #region Creation

            public CreateCharacter(string category, string name, Attributes? attributes) : base(category)
            {
                Name = name;
                Attributes = attributes;
            }

            #endregion

            #region Public Interface

            public Attributes? Attributes { get; set; }
            public string Name { get; init; }

            public void Deconstruct(out string name)
            {
                name = Name;
            }

            #endregion
        }

        public record SetAttributes : Command
        {
            #region Creation

            public SetAttributes(Guid entityId, Attributes attributes) : base(
                new StreamId(CharacterManagementService.Category, entityId)
            )
            {
                Attributes = attributes;
            }

            #endregion

            #region Public Interface

            public Attributes Attributes { get; init; }

            public void Deconstruct(out StreamId entityStreamId, out Attributes attributes)
            {
                entityStreamId = GetEntityStreamId();
                attributes = Attributes;
            }

            #endregion
        }
    }
}
