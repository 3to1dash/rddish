using System.Data;
using System.Diagnostics;
using Dapper;
using rddish.Radius.Data;
using rddish.Radius.Entities;

namespace rddish.Radius;

public interface IUnitOfWork
{
    IEnumerable<radacct>? GetUsers();
    Task<int> AddNas(NasDto input);
    Task<int> UpdateNas(int nasId, NasDto input);
    Task<int> DisableSimultaneous(string userName);
    void Commit();
    void Rollback();
    void Dispose();
}

public class UnitOfWork : IDisposable, IUnitOfWork
{
    private bool _disposed;
    private IDbConnection? _connection;
    private IDbTransaction? _transaction;

    public UnitOfWork(IDbConnection connection)
    {
        _connection = connection;
    }

    internal UnitOfWork(
        IDbConnection connection,
        bool transactional = false,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        _connection = connection;

        if (transactional)
            _transaction = connection.BeginTransaction(isolationLevel);
    }

    public IEnumerable<radacct>? GetUsers()
    {
        Debug.Assert(_connection != null, "There are no database connections");
        const string sql = "SELECT * FROM radacct";
        return _connection.Query<radacct>(sql);
    }

    public async Task<int> AddNas(NasDto input)
    {
        Debug.Assert(_connection != null, "There are no database connections");
        const string sql = """
                           INSERT INTO nas
                               (nasname, shortname, type, ports, secret, community, description) VALUES
                               (@nasname, @shortname, @type, @ports, @secret, @community, @description)
                           """;
        var result = await _connection.ExecuteAsync(sql, input);
        return result;
    }

    public async Task<int> UpdateNas(int nasId, NasDto input)
    {
        Debug.Assert(_connection != null, "There are no database connections");
        const string sql = """
                           UPDATE nas SET
                               nasname = @nasname,
                               shortname = @shortname,
                               type = @type,
                               ports = @ports,
                               secret = @secret,
                               community = @community,
                               description = @description
                           WHERE id = @nasId
                           """;
        return await _connection.ExecuteAsync(sql, new { nasId, input });
    }

    public async Task<int> DisableSimultaneous(string userName)
    {
        Debug.Assert(_connection != null, "There are no database connections");
        const string sql = """
                           INSERT INTO radcheck (username,attribute,op,value)
                           VALUES (@UserName, 'Simultaneous-Use', ':=', '1')
                           """;
        return await _connection.ExecuteAsync(sql, new { UserName = userName });
    }

    public void Commit()
        => _transaction?.Commit();

    public void Rollback()
        => _transaction?.Rollback();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~UnitOfWork()
        => Dispose(false);

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }

        _transaction = null;
        _connection = null;

        _disposed = true;
    }
}