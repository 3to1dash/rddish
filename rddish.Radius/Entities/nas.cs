using System.ComponentModel.DataAnnotations;

namespace rddish.Radius.Entities;

public class nas
{
    [Key] [DataType("int(10)")] public int id { get; set; }
    [DataType("varchar(128)")] public string nasname { get; set; }
    [DataType("varchar(32)")] public string? shortname { get; set; }
    [DataType("varchar(30)")] public string? type { get; set; } = "other";
    [DataType("int(5)")] public int? ports { get; set; }
    [DataType("varchar(60)")] public string secret { get; set; } = "secret";
    [DataType("varchar(64)")] public string? server { get; set; }
    [DataType("varchar(50)")] public string? community { get; set; }
    [DataType("varchar(200)")] public string? description { get; set; } = "RADIUS Client";
}