namespace rddish.API.Data.RequestDtos;

public class RadiusClient
{
    public string Services { get; set; }
    public string Address { get; set; }
    public string Secret { get; set; }
}

public class IpPool
{
    public string Name { get; set; }
    public string FromIp { get; set; }
    public string ToIp { get; set; }
}