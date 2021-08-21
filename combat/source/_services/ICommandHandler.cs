namespace EventSourcingDemo.Combat
{
    public interface ICommandHandler<in T>
    {
        public Result Handle(T command);
    }
}
