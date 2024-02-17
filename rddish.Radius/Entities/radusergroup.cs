using System.ComponentModel.DataAnnotations;

namespace rddish.Radius.Entities;

public class radusergroup
{
    [Key] [DataType("int(11)")] public int id { get; set; }
    [DataType("varchar(64)")] public string username { get; set; }
    [DataType("varchar(64)")] public string groupname { get; set; }
    [DataType("int(11)")] public int priority { get; set; } = 1;
}