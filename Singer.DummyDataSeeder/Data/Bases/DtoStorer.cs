namespace Singer.DummyDataSeeder.Data.Bases
{
    internal class DtoStorer<TDto, TCreateDto> : IDtoStorer<TDto, TCreateDto>
    {
        public TDto Dto { get; set; }
        public TCreateDto CreateDto { get; set; }
    }
}
