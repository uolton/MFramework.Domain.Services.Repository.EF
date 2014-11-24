namespace MFramework.Domain.Services.Repository.EF
{
    public class EFFetchingRepository<TEntity, TReleated> : RepositoryWrapperBase<EFRepository<TEntity>, TEntity>, IEFFetchingRepository<TEntity, TReleated> where TEntity :Entity<TEntity>,new()
    {
        readonly string _fetchingPath;

        public EFFetchingRepository(EFRepository<TEntity> repository, string fetchingPath) : base(repository)
        {
            _fetchingPath = fetchingPath;
        }

        public string FetchingPath
        {
            get { return _fetchingPath; }
        }
    }
}