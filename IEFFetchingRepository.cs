namespace MFramework.Domain.Services.Repository.EF
{
    public interface IEFFetchingRepository<TEntity, TFetch> : IRepository<TEntity> where TEntity : Entity<TEntity>,new()
    {
        EFRepository<TEntity> RootRepository { get; }

        string FetchingPath { get; }
    }
}