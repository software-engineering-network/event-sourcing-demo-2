using System;

namespace EventSourcingDemo.Combat
{
    public record SetAttributes : ICommand
    {
        #region Creation

        public SetAttributes(
            Guid characterId,
            ushort attack,
            ushort defense,
            ushort hitPoints,
            ushort magicAttack,
            ushort magicDefense,
            ushort speed
        )
        {
            Id = Guid.NewGuid();
            CharacterId = characterId;
            Attributes = new Attributes(attack, defense, hitPoints, magicAttack, magicDefense, speed);
        }

        #endregion

        #region Public Interface

        public Attributes Attributes { get; }
        public Guid CharacterId { get; }
        public Guid Id { get; }

        #endregion

        public class Handler : ICommandHandler<SetAttributes>
        {
            private readonly ICharacterRepository _characterRepository;

            #region Creation

            public Handler(ICharacterRepository characterRepository)
            {
                _characterRepository = characterRepository;
            }

            #endregion

            #region ICommandHandler<SetAttributes> Implementation

            public Result Handle(SetAttributes command)
            {
                var character = _characterRepository
                    .Find(command.CharacterId)
                    .SetAttributes(command.Attributes);

                _characterRepository.Update(character);

                return new(character.Id, Status.Succeeded);
            }

            #endregion
        }
    }
}
