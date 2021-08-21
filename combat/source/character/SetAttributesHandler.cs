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

        public Result Handle(SetAttributes command) => new(Status.Succeeded);

        #endregion
    }
}
