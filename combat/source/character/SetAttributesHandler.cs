namespace EventSourcingDemo.Combat
{
    public class SetAttributesHandler : ICommandHandler<SetAttributes>
    {
        private readonly ICharacterRepository _characterRepository;

        #region Creation

        public SetAttributesHandler(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        #endregion

        #region ICommandHandler<SetAttributes> Implementation

        public Result Handle(SetAttributes command)
        {
            var (characterId, attack, defense, hitPoints, magicAttack, magicDefense, speed) = command;
            var character = _characterRepository.Find(characterId);
            var attributes = new Attributes(attack, defense, hitPoints, magicAttack, magicDefense, speed);

            character.SetAttributes(attributes);

            var updatedCharacter = _characterRepository
                .Save(character)
                .Find(characterId);

            return new(Status.Succeeded);
        }

        #endregion
    }
}
