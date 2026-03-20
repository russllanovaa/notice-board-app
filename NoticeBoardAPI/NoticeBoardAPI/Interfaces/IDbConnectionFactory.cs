using System.Data;

namespace NoticeBoardAPI.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
