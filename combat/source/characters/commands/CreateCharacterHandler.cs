namespace EventSourcingDemo.Combat
{
    public class CreateCharacterHandler : ICommandHandler<CreateCharacter>
    {
        private readonly ICharacterRepository _characterRepository;

        #region Creation

        public CreateCharacterHandler(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        #endregion

        #region ICommandHandler<CreateCharacter> Implementation

        public Result Handle(CreateCharacter command)
        {
            var character = Character.Create(command.Name);

            _characterRepository.Save(character);

            return new Result(Status.Succeeded);
        }

        #endregion
    }
}
