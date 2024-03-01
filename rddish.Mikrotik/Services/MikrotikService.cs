using MikrotikDotNet;

namespace rddish.Mikrotik.Services;

public interface IMikrotikService
{
    bool AddRadiusClient(string services, string address, string secret);
    bool AddIpPool(string name, string fromIp, string toIp);
}

public class MikrotikService : IMikrotikService
{
    private readonly MKConnection _mikrotikConnection;

    public MikrotikService(MKConnection mikrotikConnection)
    {
        _mikrotikConnection = mikrotikConnection;
    }

    public bool AddRadiusClient(string services, string address, string secret)
    {
        _mikrotikConnection.Open();
        var cmd = _mikrotikConnection.CreateCommand("/radius add");
        cmd.Parameters.Add("service", services);
        cmd.Parameters.Add("address", address);
        cmd.Parameters.Add("secret", secret);
        cmd.ExecuteNonQuery();
        return true;
    }

    public bool AddIpPool(string name, string fromIp, string toIp)
    {
        _mikrotikConnection.Open();
        var cmd = _mikrotikConnection.CreateCommand("/ip pool add");
        cmd.Parameters.Add("name", name);
        cmd.Parameters.Add("ranges", $"{fromIp}-{toIp}");
        cmd.ExecuteNonQuery();
        return true;
    }
}