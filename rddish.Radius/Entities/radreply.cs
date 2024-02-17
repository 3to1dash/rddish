﻿using System.ComponentModel.DataAnnotations;

namespace rddish.Radius.Entities;

public class radreply
{
    [Key] [DataType("int(11)")] public int id { get; set; }
    [DataType("varchar(64)")] public string groupname { get; set; }
    [DataType("varchar(64)")] public string attribute { get; set; }
    [DataType("char(2)")] public string op { get; set; } = "=";
    [DataType("varchar(253)")] public string value { get; set; }
}