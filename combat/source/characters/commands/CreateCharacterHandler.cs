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
            var character = new Character(command.Name);

            _characterRepository.Create(character);

            return new Result(Status.Succeeded);
        }

        #endregion
    }
}
