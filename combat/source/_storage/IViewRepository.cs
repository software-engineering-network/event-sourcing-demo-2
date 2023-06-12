namespace EventSourcingDemo.Combat
{
    public interface IViewRepository
    {
        Result Create(string name, View view);
        Result<View> Find(string name);
        Result Update(string name, View view);
    }
}
