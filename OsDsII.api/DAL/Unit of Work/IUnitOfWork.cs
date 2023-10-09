namespace OsDsII.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public Task SaveChangesAsync();
    }
}