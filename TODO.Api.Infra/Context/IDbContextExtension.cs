namespace TODO.Api.Infra.Context
{
    public interface IDbContextExtension
    {
        Task<bool> Commit();
    }
}
