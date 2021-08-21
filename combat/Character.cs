using System;

namespace EventSourcingDemo.Combat
{
    public class Character
    {
        #region Creation

        public Character(Guid id = default)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
        }

        #endregion

        #region Public Interface

        public Guid Id { get; set; }
        public string Name { get; }

        #endregion
    }
}
