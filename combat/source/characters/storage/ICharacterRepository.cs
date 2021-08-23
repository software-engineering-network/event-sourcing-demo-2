using System;

namespace EventSourcingDemo.Combat
{
    public interface ICharacterRepository
    {
        ICharacterRepository Create(Character character);
        Character Find(Guid id);
        ICharacterRepository Update(Character character);
    }
}
