namespace Sub.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();

    }
}
