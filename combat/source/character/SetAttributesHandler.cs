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
            var character = _characterRepository.Find(command.CharacterId);

            character.SetAttributes(command.Attributes);

            var updatedCharacter = _characterRepository
                .Save(character)
                .Find(character.Id);

            return new(Status.Succeeded);
        }

        #endregion
    }
}
