namespace TKompTask.DataAccess
{
    public interface IDataAccessService
    {
        bool TryOpenConnection(string username, string password);
    }
}