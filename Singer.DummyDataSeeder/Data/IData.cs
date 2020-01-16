namespace Singer.DummyDataSeeder.Data
{
    public interface IData<T>
    {
        T[] Data { get; }
    }
}
