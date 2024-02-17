using System.ComponentModel.DataAnnotations;

namespace rddish.Radius.Entities;

public class radpostauth
{
    [Key] [DataType("int(11)")] public int id { get; set; }
    [DataType("varchar(64)")] public string username { get; set; }
    [DataType("varchar(64)")] public string pass { get; set; }
    [DataType("varchar(32)")] public string reply { get; set; }
    [DataType("timestamp(6)")] public DateTime authdate { get; set; }
    [DataType("varchar(64)")] public string @class { get; set; }
}