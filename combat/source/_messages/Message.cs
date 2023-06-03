using System;

namespace EventSourcingDemo.Combat
{
    public abstract record Message
    {
        #region Public Interface

        public string Category { get; init; }
        public Guid EntityId { get; init; }
        public Guid Id { get; init; } = Guid.NewGuid();
        public Metadata Metadata { get; init; }
        public Version Version { get; set; }

        #endregion
    }
}
