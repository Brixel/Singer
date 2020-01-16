namespace Singer.DummyDataSeeder.Data.Bases
{
    internal interface IDtoStorer<TDto, TCreateDto>
    {
        TDto Dto { get; set; }
        TCreateDto CreateDto { get; set; }
    }
}