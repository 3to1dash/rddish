namespace rddish.API.Data.RequestDtos;

public class AddGroupDto
{
    public string GroupName { get; set; }
    public string Attribute { get; set; }
    public string Op { get; set; }
    public string Value { get; set; }
}

public class AddUserDto
{
    public string UserName { get; set; }
    public string Attribute { get; set; }
    public string Op { get; set; }
    public string Value { get; set; }
}

public class AddUserToGroupDto
{
    public string UserName { get; set; }
    public string GroupName { get; set; }
    public int Priority { get; set; }
}