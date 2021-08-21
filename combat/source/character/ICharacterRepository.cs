using System;

namespace EventSourcingDemo.Combat
{
    public interface ICharacterRepository
    {
        Character Find(Guid id);
        ICharacterRepository Save(Character character);
    }
}
