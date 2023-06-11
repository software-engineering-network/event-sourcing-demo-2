namespace EventSourcingDemo.Combat
{
    public interface IViewRepository
    {
        Result Create(string name);
        Result<object> Find(string name);
        Result Update(string name, View view);
    }
}
