namespace Singer.DummyDataSeeder.Data.Bases
{
    internal interface IDataContainer<TDto, TCreateDto>
    {
        IDtoStorer<TDto, TCreateDto>[] Data { get; }
    }
}
