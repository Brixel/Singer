namespace Singer.DummyDataSeeder.Data.Bases
{
    public interface IData<T>
    {
        T[] Data { get; }
    }
}
