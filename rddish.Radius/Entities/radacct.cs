using System.ComponentModel.DataAnnotations;

namespace rddish.Radius.Entities;

public class radacct
{
    [Key] [DataType("bigint(21)")] public long radacctid { get; set; }
    [DataType("varchar(64)")] public string acctsessionid { get; set; }
    [DataType("varchar(32)")] public string acctuniqueid { get; set; }
    [DataType("varchar(64)")] public string username { get; set; }
    [DataType("varchar(64)")] public string groupname { get; set; }
    [DataType("varchar(64)")] public string? realm { get; set; }
    [DataType("varchar(15)")] public string nasipaddress { get; set; }
    [DataType("varchar(32)")] public string? nasportid { get; set; }
    [DataType("varchar(32)")] public string? nasporttype { get; set; }
    public DateTime? acctstarttime { get; set; }
    public DateTime? acctupdatetime { get; set; }
    public DateTime? acctstoptime { get; set; }
    [DataType("int(12)")] public int? acctinterval { get; set; }
    [DataType("int(12)")] public int? acctsessiontime { get; set; }
    [DataType("varchar(32)")] public int? acctauthentic { get; set; }
    [DataType("varchar(50)")] public int? connectinfo_start { get; set; }
    [DataType("varchar(50)")] public int? connectinfo_stop { get; set; }
    [DataType("bigint(20)")] public long? acctinputoctets { get; set; }
    [DataType("bigint(20)")] public long? acctoutputoctets { get; set; }
    [DataType("varchar(50)")] public string calledstationid { get; set; }
    [DataType("varchar(50)")] public string callingstationid { get; set; }
    [DataType("varchar(32)")] public string acctterminatecause { get; set; }
    [DataType("varchar(32)")] public string? servicetype { get; set; }
    [DataType("varchar(32)")] public string? framedprotocol { get; set; }
    [DataType("varchar(15)")] public string framedipaddress { get; set; }
    [DataType("varchar(45)")] public string framedipv6address { get; set; }
    [DataType("varchar(45)")] public string framedipv6prefix { get; set; }
    [DataType("varchar(44)")] public string framedinterfaceid { get; set; }
    [DataType("varchar(45)")] public string delegatedipv6prefix { get; set; }
    [DataType("varchar(64)")] public string? @class { get; set; }
}