namespace Singer.DummyDataSeeder.Data.Bases
{
    internal abstract class DataContainer<TDto, TCreateDto> : IDataContainer<TDto, TCreateDto>
    {
        protected static Randomizer R => Randomizer.Instance;

        public abstract IDtoStorer<TDto, TCreateDto>[] Data { get; }
    }
}
