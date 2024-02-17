using System.ComponentModel.DataAnnotations;

namespace rddish.Radius.Entities;

public class nasreload
{
    [Key] [DataType("varchar(15)")] public int nasipaddress { get; set; }
    public DateTime reloadtime { get; set; }
}