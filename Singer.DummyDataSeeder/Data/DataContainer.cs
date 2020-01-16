namespace Singer.DummyDataSeeder.Data
{
    internal abstract class DataContainer<T> : IData<T>
    {
        protected static Randomizer R => Randomizer.Instance;

        public abstract T[] Data { get; }
    }
}
