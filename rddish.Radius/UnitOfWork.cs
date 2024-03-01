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
    Task<int> AddSimultaneous(string userName, int allowedSessions);
    Task<int> AddStaticIp(string userName, string staticIp);
    Task<int> AssignUserIpPool(string userName, string ipPool);
    Task<int> AddUserExpirationDate(string userName, DateTime expirationDate);
    Task<int> AddUserRateLimit(string userName, string uploadLimit, string downloadLimit);
    Task<int> AddUserTotalLimit(string userName, int totalLimit);
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
        Debug.Assert(connection != null, "There are no database connections");
        _connection = connection;
    }

    internal UnitOfWork(
        IDbConnection connection,
        bool transactional = false,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        Debug.Assert(connection != null, "There are no database connections");
        _connection = connection;

        if (transactional)
            _transaction = connection.BeginTransaction(isolationLevel);
    }

    public IEnumerable<radacct>? GetUsers()
    {
        const string sql = "SELECT * FROM radacct";
        return _connection.Query<radacct>(sql);
    }

    public async Task<int> AddNas(NasDto input)
    {
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

    public async Task<int> AddSimultaneous(string userName, int allowedSessions)
    {
        const string sql = """
                           INSERT INTO radcheck (username,attribute,op,value)
                           VALUES (@UserName, 'Simultaneous-Use', ':=', @AllowedSessions)
                           """;
        return await _connection.ExecuteAsync(sql, new { UserName = userName, AllowedSessions = allowedSessions });
    }

    public async Task<int> AddStaticIp(string userName, string staticIp)
    {
        const string sql = """
                           INSERT INTO radreply (username,attribute,op,value)
                           VALUES (@UserName, 'Framed-IP-Address', '==', @StaticIp)
                           """;
        return await _connection.ExecuteAsync(sql, new { UserName = userName, StaticIp = staticIp });
    }

    public async Task<int> AssignUserIpPool(string userName, string ipPool)
    {
        const string sql = """
                           INSERT INTO radreply (username,attribute,op,value)
                           VALUES (@UserName, 'Framed-Pool', '==', @IpPool)
                           """;
        return await _connection.ExecuteAsync(sql, new { UserName = userName, IpPool = ipPool });
    }

    public async Task<int> AddUserExpirationDate(string userName, DateTime expirationDate)
    {
        var formattedDatetime = expirationDate.ToString("d MMM yyyy HH:mm");
        const string sql = """
                           INSERT INTO radcheck (username,attribute,op,value)
                           VALUES (@UserName, 'Expiration', ':=', @ExpirationDate)
                           """;
        return await _connection.ExecuteAsync(sql, new { UserName = userName, ExpirationDate = expirationDate });
    }

    public async Task<int> AddUserRateLimit(string userName, string uploadLimit, string downloadLimit)
    {
        const string sql = """
                           INSERT INTO radreply (username,attribute,op,value)
                           VALUES (@UserName, 'Mikrotik-Rate-Limit', ':=', @BandwidthLimit)
                           """;
        return await _connection.ExecuteAsync(sql,
            new { UserName = userName, BandwidthLimit = $"{uploadLimit}/{downloadLimit}" });
    }

    public async Task<int> AddUserTotalLimit(string userName, int totalLimit)
    {
        const string sql = """
                           INSERT INTO radcheck (username,attribute,op,value)
                           VALUES (@UserName, 'Mikrotik-Total-Limit', ':=', @TotalLimit)
                           """;
        return await _connection.ExecuteAsync(sql,
            new { UserName = userName, TotalLimit = totalLimit });
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