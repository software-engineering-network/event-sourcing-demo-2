namespace EventSourcingDemo.Combat
{
    public record CreateCharacter : ICommand
    {
        #region Creation

        public CreateCharacter(string name)
        {
            Name = name;
        }

        #endregion

        #region Public Interface

        public string Name { get; }

        #endregion

        public class Handler : ICommandHandler<CreateCharacter>
        {
            private readonly ICharacterRepository _characterRepository;

            #region Creation

            public Handler(ICharacterRepository characterRepository)
            {
                _characterRepository = characterRepository;
            }

            #endregion

            #region ICommandHandler<CreateCharacter> Implementation

            public Result Handle(CreateCharacter command)
            {
                _characterRepository.Create(new Character(command.Name));

                return new Result(Status.Succeeded);
            }

            #endregion
        }
    }
}
